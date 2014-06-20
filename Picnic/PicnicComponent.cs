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
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using IronPython;
using IronPython.Modules;
using IronPython.Compiler.Ast;
using IronPython.Runtime.Binding;
using IronPython.Runtime;
using IronPython.Runtime.Exceptions;
using IronPython.Runtime.Types;
using IronPython.Runtime.Operations;
using IronPython.Compiler;
using IronPython.Hosting;



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
            pManager.Register_GenericParam("Key", "K", "Key to check.", GH_ParamAccess.item);
            pManager.Register_GenericParam("Func", "F", "Function to Input", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.Register_BooleanParam("Contains", "B", "Contains Key.", GH_ParamAccess.item);
            pManager.Register_StringParam("Keys", "KS", "Keys in Picnic", GH_ParamAccess.list);
            pManager.Register_GenericParam("Out", "O", "jhfjgs", GH_ParamAccess.item);
            pManager.Register_StringParam("S", "S", "S", GH_ParamAccess.item);
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
            //Func<object, object> f = new Func<object, object>();
            GH_ObjectWrapper f = new GH_ObjectWrapper();
            //object f = new object();
            GH_Point p = new GH_Point();
            //double x = p.Value.X;

            //object key_str = new object();
            GH_ObjectWrapper key_str = new GH_ObjectWrapper();



            if (!DA.GetData(0, ref key_str)) return;
            if (!DA.GetData(1, ref f)) return;

            //var ret = key_str.Value;
            var ret = key_str;
            Type t =ret.GetType();

            ScriptEngine pyEngine = Python.CreateEngine();
            //delegate new_func = f.CastTo<Func<object, object>(out object);

            dynamic result = pyEngine.Operations.Invoke(f.Value, ret);
            object return_object = (object) result;
            //Grasshopper.Kernel.Types.GH_ObjectWrapper wrapper = new GH_ObjectWrapper(return_object);
            //res = wrapper;




            for (int i = 0; i < 10; i++)
            {
                vals.Add(i);
            }


            //Picnic test = new Picnic();

            //test.add("a", vals);

            //test.add("b", vals);

           // test_bool = true;

            //output = test.getkeys();
            string out_str = return_object.GetType().ToString();

            SpatialGraph gph = new SpatialGraph();

            DA.SetData(0, test_bool);
            DA.SetDataList(1, output);
            DA.SetData(2, return_object);
            DA.SetData(3, out_str);

            
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
