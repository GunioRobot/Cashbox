﻿// Copyright (c) 2010 Travis Smith
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
namespace Cashbox.Engines.FileStorage
{
	using System.IO;


	public class StreamStorage
	{
		readonly Stream _dataStream;

		public StreamStorage(Stream dataStream)
		{
			_dataStream = dataStream;

			// reset stream to the start
			_dataStream.SeekStart();

			if (_dataStream.Length > 0)
				LoadHeader();
			else
				WriteNewHeader();
		}

		public StreamHeader Header { get; set; }

		void WriteNewHeader()
		{
			Header = new StreamHeader
				{
					Version = 1
				};

			_dataStream.WriteStreamHeader(Header);
		}

		void LoadHeader()
		{
			var buffer = new byte[1];
			_dataStream.Read(buffer, 0, 1);

			Header = new StreamHeader
				{
					Version = buffer[0]
				};
		}


		public long Store(RecordHeader header, byte[] data)
		{
			_dataStream.SeekStart();
			_dataStream.WriteRecordHeader(header);
			_dataStream.Write(data);
			return _dataStream.Position;
		}
	}
}