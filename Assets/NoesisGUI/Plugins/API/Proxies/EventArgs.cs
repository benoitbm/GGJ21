//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.10
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


using System;
using System.Runtime.InteropServices;

namespace Noesis
{

public class EventArgs : IDisposable {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal EventArgs(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(EventArgs obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~EventArgs() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          NoesisGUI_PINVOKE.delete_EventArgs(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

  internal static void InvokeHandler(Delegate handler, IntPtr sender, IntPtr args) {
    EventHandler handler_ = (EventHandler)handler;
    if (handler_ != null) {
      handler_(Extend.GetProxy(sender, false), new EventArgs(args, false));
    }
  }

  private static EventArgs _empty = GetEmptyHelper();
  public static EventArgs Empty { get { return _empty; } }


  private static EventArgs GetEmptyHelper() {
    IntPtr cPtr = NoesisGUI_PINVOKE.EventArgs_GetEmptyHelper();
    EventArgs ret = (cPtr == IntPtr.Zero) ? null : new EventArgs(cPtr, false);
    return ret;
  }

  public EventArgs() : this(NoesisGUI_PINVOKE.new_EventArgs(), true) {
  }

}

}

