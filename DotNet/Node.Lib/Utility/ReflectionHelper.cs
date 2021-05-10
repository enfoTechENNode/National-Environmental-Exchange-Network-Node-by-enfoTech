using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Drawing;
using System.Data;
using System.Web.UI.WebControls;

namespace Node.Lib.Utility
{
	/// <summary>
	/// Utility function to help you set property of an objct using reflection.
	/// </summary>
	public sealed class ReflectionHelper 
    { 
        private ReflectionHelper() 
		{} 

        /// <summary>
        /// Set value of an object through reflection.
        /// </summary>
        /// <param name="o">oject you are going to set value to.</param>
        /// <param name="name">property of the object</param>
        /// <param name="value">value you want to set</param>
        /// <returns>orginal object</returns>
		public static object SetPropertyValue(object o, string name, object value) 
        {
			return SetPropertyValue(o, name, value, null);
        }

		/// <summary>
		/// Set value of an object through reflection.
		/// Note: the value can only be set when the property is not null and writeable.
		/// </summary>
		/// <param name="o">oject you are going to set value to.</param>
		/// <param name="name">property of the object</param>
		/// <param name="value">value you want to set</param>
		/// <param name="index">parameter of property</param>
		/// <returns>orginal object</returns>		
		public static object SetPropertyValue(object o, string name, object value, object[] index) 
        { 
			PropertyInfo pi = o.GetType().GetProperty(name);
			if (pi != null && pi.CanWrite)
			{
				Type pt = pi.PropertyType;

				if (pt == typeof(string))
					pi.SetValue(o, "" + value, index);
				else if (pt == typeof(int))
					pi.SetValue(o, int.Parse("" + value), index);
				else if (pt == typeof(bool))
					pi.SetValue(o, bool.Parse("" + value), index);
				else if (pt.BaseType == typeof(Enum))
					pi.SetValue(o, Enum.Parse(pt, "" + value), index);
				else if (pt == typeof(Unit))
					pi.SetValue(o, Unit.Parse(""+value), index);
				else if (pt == typeof(Color))
					pi.SetValue(o, Color.FromName(""+value), index);
				else
					pi.SetValue(o, value, index);
			}
			return o;
		}

		/// <summary>
		/// Add event handler based on method name.
		/// </summary>
		/// <param name="target">oject you are going to add event handler</param>
		/// <param name="eventName">Name of event</param>
		/// <param name="userControl">The control object where the method is going to be invoked.</param>
		/// <param name="method">invoke method</param>
		/// <returns></returns>
		public static object AddEventHandler(object target, string eventName, object userControl, string method)
		{
			EventInfo ei = target.GetType().GetEvent(eventName);
			if (ei != null)
			{
				Delegate handler = Delegate.CreateDelegate(ei.EventHandlerType, userControl, method);
				ei.AddEventHandler(target, handler);
			}
			return target;
		}

    } 
}
