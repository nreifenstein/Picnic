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
    public class AntPicnic :  GH_Component {
   
        protected PythonScript _py;
        private PythonCompiledCode _compiled_py;
        readonly StringList m_py_output = new StringList(); // python output stream is piped to m_py_output

        public AntPicnic()
            //Call the base constructor
            : base("Ants Picnic", "AP", "Creates a time history sequence. Build 06.07.14.", "Ants", "Picnic") { }
        public override Grasshopper.Kernel.GH_Exposure Exposure { get { return GH_Exposure.primary; } }
        public override Guid ComponentGuid { get { return new Guid("{F2019D6E-2858-4C75-AE7D-101D7E54D612}"); } }

        //protected override Bitmap Icon { get { return Ants.Properties.Resources.Ants_Icons_ant_world; } }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager) {
            pManager.RegisterParam(new GHParam_SpatialGraph(), "Spatial Graph", "S", "The Spatial Graph.", GH_ParamAccess.item);
            pManager.Register_StringParam("Script", "Func", "The function to execute", GH_ParamAccess.item);
            pManager.Register_GenericParam("Value", "V", "Initial Values", GH_ParamAccess.list);
            pManager.Register_IntegerParam("Generations", "G", "Number of Generations to create.", 0, GH_ParamAccess.item);
            pManager.Register_GenericParam("Dict", "D", "Dictionary", GH_ParamAccess.item);
            pManager.Register_StringParam("Key", "K", "Key", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager) {
            pManager.Register_StringParam("Console", "...", "Messages from Python", GH_ParamAccess.tree);
            pManager.RegisterParam(new GHParam_AWorld(), "AWorld", "W", "The resulting AntsWorld.", GH_ParamAccess.item);
            pManager.Register_StringParam("Value", "V", "Value for Key", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA) {
            AWorld refwrld = new AWorld();
            List<object> v_list = new List<object>();
            GH_ObjectWrapper gh_dict = new GH_ObjectWrapper();
            IDictionary<string, string> dict = new Dictionary<string,string>();
            string k = "";
            
            //GH_Dict test = GH_Dict.create("a", 1.0);

            //if (!DA.GetData(0, ref refwrld) || !refwrld.IsValid) return;
            //AWorld wrld = new AWorld(refwrld);

            SpatialGraph gph = new SpatialGraph();
            if (!DA.GetData(0, ref gph)) return;

            int nGen = 0;
            string pyString = "";
            if (!DA.GetData(1, ref pyString)) return;
            if (!DA.GetDataList(2, v_list)) return;
            if (!DA.GetData(3, ref nGen)) return;
            if (!DA.GetData(4, ref gh_dict)) return;
            if (!DA.GetData(5, ref k)) return;

            //dict = (IDictionary<string, string>)gh_dict.Value;

            //object d = gh_dict.Value;
            var t = Type.GetType("IronPython.Runtime.PythonDictionary,IronPython");
            IDictionary i_dict = Activator.CreateInstance(t) as IDictionary;
            //IronPython.Runtime.PythonDictionary d = gh_dict.Value;
            //string v = d.get(k);
            string v = "oops";

            // Sets the initial Generation by using the input v_list
            // if it runs out of values, it starts over (wraps)
            //object[] val_list = new object[gph.nodes.Count];
            //int v_i = 0;
            //for (int i = 0; i < gph.nodes.Count; i++)
            //{
            //    if (v_i == v_list.Count) v_i = 0;
            //    val_list[i] = v_list[v_i];
            //    v_i++;
            //}

            //AWorld wrld = new AWorld(gph, val_list);

            //_py = PythonScript.Create();
            //_py.Output = this.m_py_output.Write;
            //_compiled_py = _py.Compile(pyString);

            //// console out
            Grasshopper.Kernel.Data.GH_Structure<Grasshopper.Kernel.Types.GH_String> consoleOut = new Grasshopper.Kernel.Data.GH_Structure<Grasshopper.Kernel.Types.GH_String>();

            //// Main evaluation cycle
            //// Should move code into the Antsworld Class
            //for (int g = 0; g < nGen; g++)
            //{
            //    // console out
            //    this.m_py_output.Reset();

            //    double[] new_vals = new double[wrld.NodeCount];
            //    for (int i = 0; i < wrld.NodeCount; i++)
            //    {
            //        int[] neighboring_indices = wrld.gph.NeighboringIndexesOf(i);

            //        // build list of neighboring values
            //        List<double> neighboring_vals = new List<double>();
            //        for (int k = 0; k < neighboring_indices.Length; k++) neighboring_vals.Add(wrld.LatestGen[neighboring_indices[k]]);


            //        double d = EvaluateCell(i, wrld.LatestGen[i], neighboring_vals);
            //        //double d = g + i + 0.0;

            //        new_vals[i] = d;
            //    }
            //    wrld.AddGen(new_vals);

            //    // console out
            //    Grasshopper.Kernel.Data.GH_Path key_path = new Grasshopper.Kernel.Data.GH_Path(g);
            //    List<Grasshopper.Kernel.Types.GH_String> gh_strs = new List<Grasshopper.Kernel.Types.GH_String>();
            //    foreach (String str in this.m_py_output.Result) gh_strs.Add(new Grasshopper.Kernel.Types.GH_String(str));
            //    consoleOut.AppendRange(gh_strs, key_path);


            //}

            DA.SetDataTree(0, consoleOut);
            //DA.SetData(1, wrld);
            DA.SetData(2, v);

        }

        private double EvaluateCell(int cell_index, double cur_val, List<double> n_vals) {
            //Dictionary<string, double> h_dict;
            var t = Type.GetType("IronPython.Runtime.List,IronPython");
            IList n_list = Activator.CreateInstance(t) as IList;
            foreach (double d in n_vals) {
                object cast = d;
                n_list.Add(cast);
            }

            Dictionary<string, double> h_dict = new Dictionary<string, double>();
            h_dict.Add("a", 1.0);

            GH_Dict test = GH_Dict.create("a", cur_val);


            _py.SetVariable("n_vals", n_list);
            _py.SetVariable("h_val", cur_val);
            _py.SetVariable("h_idx", cell_index);
            _py.SetVariable("h_dict", h_dict);
            _py.SetVariable("test", test);

            try {
                _compiled_py.Execute(_py);
            } catch(Exception ex) {
                AddErrorNicely(m_py_output, ex);
            }
            //object o = _py.GetVariable("h_val");
            GH_Dict out_dict = (GH_Dict) _py.GetVariable("test");
            var o = out_dict.val["b"];
            return System.Convert.ToDouble(o);
        }


       
        private void AddErrorNicely(StringList sw, Exception ex) {
            sw.Write(string.Format("Runtime error ({0}): {1}", ex.GetType().Name, ex.Message));

            string error = _py.GetStackTraceFromException(ex);

            error = error.Replace(", in <module>, \"<string>\"", ", in script");
            error = error.Trim();

            sw.Write(error);
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
