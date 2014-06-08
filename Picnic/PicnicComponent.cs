using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using System;
using System.Drawing;
using Rhino;
using Rhino.Runtime;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Ants;
using GH_IO;

namespace Picnic
{
    public class PicnicComponent : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public PicnicComponent()
            : base("Picnic", "PN",
                "Description",
                "Ants", "Picnic")
                    {
        }

        public override Grasshopper.Kernel.GH_Exposure Exposure { get { return GH_Exposure.primary; } }
        //public override Guid ComponentGuid { get { return new Guid("{D17343E8-0BD8-4F32-9DF7-B516D52EC585}"); } }


        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.Register_StringParam("Key", "K", "Key to check.", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.Register_BooleanParam("Contains", "B", "Contains Key.", GH_ParamAccess.item);
            pManager.Register_StringParam("Keys", "KS", "Keys in Picnic", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            bool test_bool = false;
            List<object> vals = new List<object>();
            List<string> output = new List<string>();

            string key_str = "";
            if (!DA.GetData(0, ref key_str)) return;

            for (int i = 0; i < 10; i++)
            {
                vals.Add(i);
            }


            Picnic test = new Picnic();

            test.add("a", vals);

            test.add("b", vals);

            test_bool = test.contains(key_str);

            output = test.getkeys();

            SpatialGraph gph = new SpatialGraph();

            DA.SetData(0, test_bool);
            DA.SetDataList(1, output);

            
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{3B82EDC5-2300-4FEB-A1D7-AC314B4BE51D}"); }
        }
    }
}
