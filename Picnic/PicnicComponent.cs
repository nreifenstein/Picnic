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
            pManager.Register_GenericParam("Key", "K", "Keys to check.", GH_ParamAccess.list);
            pManager.Register_GenericParam("Func", "F", "Function to Input", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.Register_StringParam("Console", "...", "Messages from Python", GH_ParamAccess.tree);
            //pManager.Register_BooleanParam("Contains", "B", "Contains Key.", GH_ParamAccess.item);
            pManager.Register_GenericParam("Keys", "KS", "Keys in Picnic", GH_ParamAccess.list);
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
            List<GH_ObjectWrapper> vals = new List<GH_ObjectWrapper>();
            List<GH_ObjectWrapper> output = new List<GH_ObjectWrapper>();
            GH_ObjectWrapper f = new GH_ObjectWrapper();
            StringList m_py_output = new StringList(); // python output stream is piped to m_py_output


            Grasshopper.Kernel.Data.GH_Structure<Grasshopper.Kernel.Types.GH_String> consoleOut = new Grasshopper.Kernel.Data.GH_Structure<Grasshopper.Kernel.Types.GH_String>();


            GH_ObjectWrapper key_str = new GH_ObjectWrapper();

            //if (!DA.GetData(0, ref key_str)) return;
            if (!DA.GetDataList(0, vals)) return;
            if (!DA.GetData(1, ref f)) return;

            var ret = key_str;
            Type t =ret.GetType();

            ScriptEngine pyEngine = Python.CreateEngine();

            //var outputStream = m_py_output.Write;
            var outputStream = new System.IO.MemoryStream();
            var outputStreamWriter = new System.IO.StreamReader(outputStream);
            //pyEngine.Runtime.IO.SetOutput(outputStream, outputStreamWriter);
            //pyEngine.Runtime.IO.SetOutput(m_py_output.Write);
            //pyEngine. = m_py_output.Write;
            PythonScript _py = PythonScript.Create();
            //_py.Output = m_py_output.Write;

            Rhino.RhinoApp.WriteLine("Testing, testong");
            
            //var textWriter = new System.IO.TextWriter;
            //pyEngine.Runtime.IO.RedirectToConsole();
            //Console.SetOut(TextWriter.Synchronized(textWriter)); 

            //System.IO.Stream out_string = pyEngine.Runtime.IO.OutputStream;
            //pyEngine.Runtime.IO.RedirectToConsole();
            //pyEngine.Runtime.IO.SetOutput
            //System.IO.TextWriter test = Console.Out;
            
            //Console.SetOut(test);
            //pyEngine. = this.m_py_output.Write;
            //_py = PythonScript.Create();

            //pyEngine.Operations.Output = this.m_py_output.Write;

            for (int i = 0; i < vals.Count; i++)
            {
                //ret = vals[i];
                Type the_type = vals[i].Value.GetType();

                dynamic result = pyEngine.Operations.Invoke(f.Value, vals[i].Value);

                object return_object = (object)result;

                GH_ObjectWrapper temp = new GH_ObjectWrapper(return_object);

                var temp2 = temp.Value;

                output.Add(new GH_ObjectWrapper(return_object));

            }



            //string out_str = return_object.GetType().ToString();

            SpatialGraph gph = new SpatialGraph();

            DA.SetData(0, test_bool);
            DA.SetDataList(1, output);
            //DA.SetData(2, return_object);
            //DA.SetData(3, out_str);

            
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
            get { return new Guid("{D17343E8-0BD8-4F32-9DF7-B516D52EC585}"); }
        }
    }

    public class Picnic_Food : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public Picnic_Food()
            : base("Picnic Food", "PF",
                "Description",
                "Ants", "Picnic")
        {
        }

        public override Grasshopper.Kernel.GH_Exposure Exposure { get { return GH_Exposure.primary; } }
        //public override Guid ComponentGuid { get { return new Guid("{BF5B1E61-F880-44C9-98C0-4A6C34B1F797}"); } }


        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.Register_GenericParam("PF", "PF", "Picnic to add to.", GH_ParamAccess.list);
            //pManager.Register_GenericParam("PF", "PF", "Picnic to add to.", GH_ParamAccess.item);
            pManager.Register_GenericParam("Key", "K", "Key to check.", GH_ParamAccess.item);
            pManager.Register_GenericParam("Vals", "V", "Values", GH_ParamAccess.list);
            pManager[0].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.Register_GenericParam("Out", "O", "jhfjgs", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<Dictionary<string, object>> pf_in = new List<Dictionary<string,object>>();
            string key_in = "";
            List<object> v_in = new List<object>();


            List<GH_ObjectWrapper> raw_pf_in = new List<GH_ObjectWrapper>();


            if (!DA.GetData(1, ref key_in)) return;
            if (!DA.GetDataList(2, v_in)) return;


            //try  {
            //    DA.GetDataList(0, pf_in);
            //}
            //catch {
            //    for (int i = 0; i < v_in.Count; i++)
            //    {
            //        pf_in.Add(new Dictionary<string, object>());
            //    }
            //}

            if (this.Params.Input[0].SourceCount == 0)
            {
                for (int i = 0; i < v_in.Count; i++)
                {
                    pf_in.Add(new Dictionary<string, object>());
                }
            }
            else
            {
                if (!DA.GetDataList(0, raw_pf_in)) return;
                for (int i = 0; i < v_in.Count; i++)
                {
                    var o = raw_pf_in[i].Value;
                    pf_in.Add(o as Dictionary<string,object>);
                }
            }

            var name = v_in[0].GetType().ToString();

            if (name.Contains("GH_Number"))
            {
                List<double> d_in = new List<double>();
                if (!DA.GetDataList(2, d_in)) return;
                List<object> new_v_in = new List<object>();
                for (int i = 0; i < d_in.Count; i++) {
                    new_v_in.Add(d_in[i]);
                }
                v_in = new_v_in;

            }

            for (int i = 0; i < v_in.Count; i++)
            {

                if (pf_in[i].ContainsKey(key_in))
                {
                    pf_in[i][key_in] = v_in[i];
                }
                else
                {
                    pf_in[i].Add(key_in, v_in[i]);
                }
            }

            List<GH_ObjectWrapper> pf_out = new List<GH_ObjectWrapper>();

            for (int i = 0; i < pf_in.Count; i++) {
                pf_out.Add(new GH_ObjectWrapper(pf_in[i]));
            }

            DA.SetDataList(0, pf_out);

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
            get { return new Guid("{BF5B1E61-F880-44C9-98C0-4A6C34B1F797}"); }
        }
    }

    public class Picnic_Query : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public Picnic_Query()
            : base("Picnic Query", "PQ",
                "Description",
                "Ants", "Picnic")
        {
        }

        public override Grasshopper.Kernel.GH_Exposure Exposure { get { return GH_Exposure.primary; } }
        //public override Guid ComponentGuid { get { return new Guid("{BF5B1E61-F880-44C9-98C0-4A6C34B1F797}"); } }


        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            //pManager.Register_GenericParam("PF", "PF", "Picnic to view.", GH_ParamAccess.list);
            //pManager.Register_GenericParam("G", "G", "Object to test.", GH_ParamAccess.item);
            pManager.Register_GenericParam("G", "G", "Object to test.", GH_ParamAccess.list);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            //pManager.Register_GenericParam("Keys", "K", "jhfjgs", GH_ParamAccess.list);
            //pManager.Register_GenericParam("Values","V","values", GH_ParamAccess.list);
            pManager.Register_GenericParam("S", "S", "Type of object.", GH_ParamAccess.item);
            pManager.Register_GenericParam("V", "V", "value", GH_ParamAccess.list);

        }

    


        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            //object obj_in = new object();
            //object out_val = new object();
            ////GH_ObjectWrapper obj_in = new GH_ObjectWrapper();
            //List<GH_ObjectWrapper> obj_in = new List<GH_ObjectWrapper>();
            //var out_val = new List<Object>();
            List<object> obj_in = new List<object>();
            List<object> obj_out = new List<object>();

            ////if (!DA.GetData(0, ref obj_in)) return;
            if (!DA.GetDataList(0, obj_in)) return;


            obj_out = Picnic.Convert_List(obj_in);


            var t_string = obj_in[0].GetType().ToString();
            DA.SetData(0, t_string);
            DA.SetDataList(1, obj_out);
            

            //List<Dictionary<string, object>> pf_in = new List<Dictionary<string, object>>();
            //List<string> key_out = new List<string>();
            //List<object> v_out = new List<object>();

            //GH_ObjectWrapper key_str = new GH_ObjectWrapper();

            //List<GH_ObjectWrapper> raw_pf_in = new List<GH_ObjectWrapper>();

            //if (!DA.GetDataList(0, raw_pf_in)) return;

            //for (int i = 0; i < raw_pf_in.Count; i++)
            //{
            //    var o = raw_pf_in[i].Value;
            //    pf_in.Add(o as Dictionary<string, object>);
            //}

            //foreach (string key in pf_in[0].Keys) {
            //    key_out.Add(key);
            //}

            //DA.SetDataList(0, key_out);

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
            get { return new Guid("{0CF76DFB-5F59-4735-80B4-215C6E2922BC}"); }
        }
    }

    public class Picnic_Get : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public Picnic_Get()
            : base("Picnic Get", "PG",
                "Description",
                "Ants", "Picnic")
        {
        }

        public override Grasshopper.Kernel.GH_Exposure Exposure { get { return GH_Exposure.primary; } }
        //public override Guid ComponentGuid { get { return new Guid("{BF5B1E61-F880-44C9-98C0-4A6C34B1F797}"); } }


        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.Register_GenericParam("PF", "PF", "Picnic to add to.", GH_ParamAccess.list);
            pManager.Register_GenericParam("K", "Key", "Key to Query", GH_ParamAccess.item);

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.Register_GenericParam("Values", "V", "values", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<Dictionary<string, object>> pf_in = new List<Dictionary<string, object>>();
            string key_in = "";
            List<object> v_out = new List<object>();

            GH_ObjectWrapper key_str = new GH_ObjectWrapper();


            if (!DA.GetDataList(0, pf_in)) return;
            if (!DA.GetData(1, ref key_in)) return;

            //for (int i = 0; i < pf_in.Count; i++)
            //{
            //    var d = pf_in[i];
            //    foreach (object key in d.Keys)
            //    {
            //        var a = key;

            //        var h1 = a.GetHashCode();
            //        var h2 = key_in.GetHashCode();

            //        string s1 = a.ToString();
            //        string s2 = key_in.ToString();
                    
            //        bool test =key.Equals(key_in);
            //        bool test2 = a.Equals(key);
            //        bool test3 = s1.Equals(s2);
            //        bool test4 = Object.Equals(key, key_in); 


            //    }


                //v_out.Add(pf_in[i][k[0]]);

            for (int i = 0; i < pf_in.Count; i++) {
                if (pf_in[i].ContainsKey(key_in))
                {
                    v_out.Add(pf_in[i][key_in]);
                }
            }


            DA.SetDataList(0, v_out);

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
            get { return new Guid("{C1A1F946-2BB1-4F3A-911D-F54273A4B7A5}"); }
        }
    }

     class StringList
    {
        private readonly List<string> _txts = new List<string>();

        public void Write(string s)
        {
            _txts.Add(s);
        }

        public void Reset()
        {
            _txts.Clear();
        }

        public IList<string> Result
        {
            get { return new System.Collections.ObjectModel.ReadOnlyCollection<string>(_txts); }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (string s in _txts)
                sb.AppendLine(s);
            return sb.ToString();
        }
    }


}
