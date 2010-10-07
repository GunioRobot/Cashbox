﻿// Copyright 2010 Travis Smith
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace Cashbox
{
	using System;
	using System.Collections.Generic;


	public interface DocumentSession :
		IDisposable
	{
		T Retrieve<T>(string key) where T : class;
		T RetrieveWithDefault<T>(string key, Func<T> defaultCreation) where T : class;
		void Store<T>(string key, T document) where T : class;
		IEnumerable<T> List<T>() where T : class;
		void Delete<T>(string key) where T : class;
	}
}