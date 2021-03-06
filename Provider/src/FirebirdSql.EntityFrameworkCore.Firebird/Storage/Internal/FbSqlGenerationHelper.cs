﻿/*
 *    The contents of this file are subject to the Initial
 *    Developer's Public License Version 1.0 (the "License");
 *    you may not use this file except in compliance with the
 *    License. You may obtain a copy of the License at
 *    https://github.com/FirebirdSQL/NETProvider/blob/master/license.txt.
 *
 *    Software distributed under the License is distributed on
 *    an "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either
 *    express or implied. See the License for the specific
 *    language governing rights and limitations under the License.
 *
 *    All Rights Reserved.
 */

//$Authors = Jiri Cincura (jiri@cincura.net)

using System.Text;
using Microsoft.EntityFrameworkCore.Storage;

namespace FirebirdSql.EntityFrameworkCore.Firebird.Storage.Internal
{
	public class FbSqlGenerationHelper : RelationalSqlGenerationHelper, IFbSqlGenerationHelper
	{
		public string ParameterNameMarker { get; set; }

		public FbSqlGenerationHelper(RelationalSqlGenerationHelperDependencies dependencies)
			: base(dependencies)
		{
			ParameterNameMarker = "@";
		}

		public override string GenerateParameterName(string name)
			=> ParameterNameMarker + name;

		public override void GenerateParameterName(StringBuilder builder, string name)
			=> builder.Append(ParameterNameMarker).Append(name);
	}
}
