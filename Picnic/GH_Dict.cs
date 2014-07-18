using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using System;
using System.Drawing;
using Rhino;
using Rhino.Runtime;
using Rhino.Geometry;
using System.Collections;
using System.Collections.Generic;


namespace Picnic
{
    public class GH_Dict
    {
        public Dictionary<object, object> val;
        public bool initialized;

        public GH_Dict() : base() {
            this.val = new Dictionary<object, object>();
            this.initialized = false;
        }

        public static GH_Dict create(object key, object num)
        {
            GH_Dict dict = new GH_Dict();

            dict.val.Add(key, num);

            return dict;
        }
    }

    public class Picnic
    {
        public List<Dictionary<object, object>> val;
        public bool initialized;

        public Picnic()
            : base() {
                this.val = new List<Dictionary<object, object>>();
            this.initialized = false;
        }

        public int ValCount
        {
            get { return this.val.Count; }
        }

        public List<string> getkeys()
        {
            List<string> keys = new List<string>();

            foreach (string key in this.val[0].Keys)
            {
                keys.Add(key);
            }

            return keys;

        }


        public bool contains(string key)
        {
            //Picnic new_picnic = new Picnic();

            for (int i = 0; i < this.ValCount; i++)
            {
                if (this.val[i].ContainsKey(key)) return true;
            }
            return false;
        }

        public bool add(string key, List<object> vals)
        {
            if (this.ValCount < 1)
            {
                for (int i = 0; i < vals.Count; i++)
                {
                    this.val.Add(new Dictionary<object,object>());
                }
            }

            for (int i = 0; i < this.ValCount; i++)
            {
                if (!this.val[i].ContainsKey(key)) 
                {
                    this.val[i].Add(key, vals[i]);
                }
                else
                {
                    this.val[i][key] = vals[i];
                }
            }
            
            
            return true;
        }

        /// <summary>
        ///  Generic Conversions
        /// </summary>
        /// <param name="obj_in"></param>
        /// <param name="obj_out"></param>

        public static List<object> Convert_List(List<object> obj_in)
        {
            To_Something Converter = null;
            List<object> obj_out = new List<object>();

            if (obj_in[0].GetType() == typeof(GH_Number))
            {
                Converter = To_Double;
            }

            if (obj_in[0].GetType() == typeof(GH_String))
            {
                Converter = To_String;
            }

            //switch (obj_in[0].GetType().ToString()) {
            //    case "GH_Number": 
            //        Converter = To_Double;
            //        break;
            //    case "GH_String":
            //        Converter = To_String;
            //        break;
            //}


            for (int i = 0; i < obj_in.Count; i++)
            {
                if (!(Converter == null))
                {
                    obj_out.Add(Converter(obj_in[i]));
                }
                else
                {
                    obj_out.Add(obj_in[i]);
                }
            }
            return obj_out;
        }

        public static object To_Double(object in_val)
        {
            double n = new double();
            var y = GH_Convert.ToDouble(in_val, out n, GH_Conversion.Both);
            return (object)n;
        }

        public static object To_String(object in_val)
        {
            string n = "";
            var y = GH_Convert.ToString(in_val, out n, GH_Conversion.Both);
            return (object)n;
        }


        public delegate object To_Something(object o);
        


        /// end of generic converter block
    }
}
