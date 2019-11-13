// <copyright file="FileSystemVirtualPathProvider.cs" company="James Jackson-South, Jeavon Leopold, and contributors">
// Copyright (c) James Jackson-South, Jeavon Leopold, and contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// </copyright>

using System;
using System.IO;
using System.Web.Hosting;
using Umbraco.Core.IO;

namespace Umbraco.Storage.S3
{
	internal class FileSystemVirtualFile : VirtualFile
	{
		private readonly Lazy<IFileSystem> FileSystem;
		private readonly string FileSystemPath;

		public FileSystemVirtualFile(string virtualPath, Lazy<IFileSystem> fileSystem, string fileSystemPath) : base(virtualPath)
		{
			this.FileSystem = fileSystem;
			this.FileSystemPath = fileSystemPath;
		}

		public override Stream Open()
		{
			return this.FileSystem.Value.OpenFile(FileSystemPath);
		}
	}
}