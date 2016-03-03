﻿/*
DNProj - Manage your *proj and sln with commandline.
Copyright (c) 2016 cannorin

This file is part of DNProj.

DNProj is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NX;
using System.Diagnostics;
using Mono.Options;
using Microsoft.Build.BuildEngine;

namespace DNProj
{
    public class ProjectCommand : Command
    {
        string projName;

        public ProjectCommand()
            : base("dnproj", "operate the .*proj file in the current directory.\nif no .*proj files are found, it fails.", "", "<command>", "[options]")
        {
            Options.Add("i=|input=", "specify project file, not in the current directory.", p => projName = p);
            Commands["new"] = new NewProjectCommand();
        }

        public Option<Project> GetProject()
        {
            return Environment.CurrentDirectory
                .Try(x => projName ?? Directory.GetFiles(x).Find(f => f.EndsWith("proj")))
                .Map(x =>
                {
                    var p = new Project();
                    p.Load(x);
                    return p;
                });
        }
    }
}

