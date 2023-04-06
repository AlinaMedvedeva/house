using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Forms;

namespace house
{
    internal class AdvancedCursor
    {
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        private static extern IntPtr LoadCursorFromFile(String str);


        public static Cursor Create(string filename)
        {
            IntPtr hCursor = LoadCursorFromFile(filename);
            if (!IntPtr.Zero.Equals(hCursor))
            {
                return new Cursor(hCursor);
            }
            else
            {
                throw new ApplicationException("Error " + filename);
            }
        }
    }
}
