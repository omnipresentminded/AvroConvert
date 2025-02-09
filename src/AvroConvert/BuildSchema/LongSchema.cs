﻿// Copyright (c) Microsoft Corporation
// All rights reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not
// use this file except in compliance with the License.  You may obtain a copy
// of the License at http://www.apache.org/licenses/LICENSE-2.0
// 
// THIS CODE IS PROVIDED *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
// KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY IMPLIED
// WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE,
// MERCHANTABLITY OR NON-INFRINGEMENT.
// 
// See the Apache Version 2.0 License for specific language governing
// permissions and limitations under the License.
namespace AvroConvert.BuildSchema
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     Class represents a long schema.
    ///     For more details please see <a href="http://avro.apache.org/docs/current/spec.html#schema_primitive">the specification</a>.
    /// </summary>
    public sealed class LongSchema : PrimitiveTypeSchema
    {
        internal LongSchema()
            : this(typeof(long))
        {
        }

        internal LongSchema(Type type)
            : this(type, new Dictionary<string, string>())
        {
        }

        internal LongSchema(Type type, Dictionary<string, string> attributes)
            : base(type, attributes)
        {
        }

        internal override global::AvroConvert.Schema.Schema.Type Type => global::AvroConvert.Schema.Schema.Type.Long;
    }
}
