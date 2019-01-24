/*
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirebirdSql.Data.Common
{
	internal static class ShutdownHandler
	{
		static LinkedList<Action> _poolCleanups;
		static LinkedList<Action> _fbClientShutdowns;

		static ShutdownHandler()
		{
			_poolCleanups = new LinkedList<Action>();
			_fbClientShutdowns = new LinkedList<Action>();
#if !NETSTANDARD1_6
			AppDomain.CurrentDomain.DomainUnload += (sender, e) => HandleShutdown();
			AppDomain.CurrentDomain.ProcessExit += (sender, e) => HandleShutdown();
#endif
		}

		internal static void RegisterPoolCleanup(Action action)
		{
			_poolCleanups.AddFirst(action);
		}

		internal static void RegisterFbClientShutdown(Action action)
		{
			_fbClientShutdowns.AddFirst(action);
		}

		static void HandleShutdown()
		{
			foreach (var item in _poolCleanups)
				item();
			foreach (var item in _fbClientShutdowns)
				item();
		}
	}
}
