using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace Picnic
{
    public class PicnicInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "Picnic";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return null;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("c2c8f0a3-2084-4bac-aed3-779e71546b7d");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "";
            }
        }
    }
}
