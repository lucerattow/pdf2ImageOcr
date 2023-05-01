﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pdf2Image
{
    internal class CursorWait : IDisposable
    {
        private Cursor _previousCursor;

        public CursorWait()
        {
            _previousCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
        }

        public void Dispose()
        {
            Cursor.Current = _previousCursor;
        }
    }
}
