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

public class ManipulationDeltaEventArgs : InputEventArgs {
  private HandleRef swigCPtr;

  internal ManipulationDeltaEventArgs(IntPtr cPtr, bool cMemoryOwn) : base(cPtr, cMemoryOwn) {
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(ManipulationDeltaEventArgs obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~ManipulationDeltaEventArgs() {
    Dispose();
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          NoesisGUI_PINVOKE.delete_ManipulationDeltaEventArgs(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  internal static new void InvokeHandler(Delegate handler, IntPtr sender, IntPtr args) {
    ManipulationDeltaEventHandler handler_ = (ManipulationDeltaEventHandler)handler;
    if (handler_ != null) {
      handler_(Extend.GetProxy(sender, false), new ManipulationDeltaEventArgs(args, false));
    }
  }

  public UIElement ManipulationContainer {
    get {
      return GetManipulationContainerHelper();
    }
  }

  public Point ManipulationOrigin {
    get {
      IntPtr ret = NoesisGUI_PINVOKE.ManipulationDeltaEventArgs_ManipulationOrigin_get(swigCPtr);
      if (ret != IntPtr.Zero) {
        return Marshal.PtrToStructure<Point>(ret);
      }
      else {
        return new Point();
      }
    }

  }

  public ManipulationDelta DeltaManipulation {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.ManipulationDeltaEventArgs_DeltaManipulation_get(swigCPtr);
      ManipulationDelta ret = (cPtr == IntPtr.Zero) ? null : new ManipulationDelta(cPtr, false);
      return ret;
    } 
  }

  public ManipulationDelta CumulativeManipulation {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.ManipulationDeltaEventArgs_CumulativeManipulation_get(swigCPtr);
      ManipulationDelta ret = (cPtr == IntPtr.Zero) ? null : new ManipulationDelta(cPtr, false);
      return ret;
    } 
  }

  public ManipulationVelocities Velocities {
    get {
      IntPtr cPtr = NoesisGUI_PINVOKE.ManipulationDeltaEventArgs_Velocities_get(swigCPtr);
      ManipulationVelocities ret = (cPtr == IntPtr.Zero) ? null : new ManipulationVelocities(cPtr, false);
      return ret;
    } 
  }

  public bool IsInertial {
    get {
      bool ret = NoesisGUI_PINVOKE.ManipulationDeltaEventArgs_IsInertial_get(swigCPtr);
      return ret;
    } 
  }

  public ManipulationDeltaEventArgs(object source, RoutedEvent arg1, Visual container, Point origin, ManipulationDelta delta, ManipulationDelta cumulative, ManipulationVelocities velocities, bool isInertial) : this(NoesisGUI_PINVOKE.new_ManipulationDeltaEventArgs(Noesis.Extend.GetInstanceHandle(source), RoutedEvent.getCPtr(arg1), Visual.getCPtr(container), ref origin, ManipulationDelta.getCPtr(delta), ManipulationDelta.getCPtr(cumulative), ManipulationVelocities.getCPtr(velocities), isInertial), true) {
  }

  public bool Cancel() {
    bool ret = NoesisGUI_PINVOKE.ManipulationDeltaEventArgs_Cancel(swigCPtr);
    return ret;
  }

  public void Complete() {
    NoesisGUI_PINVOKE.ManipulationDeltaEventArgs_Complete(swigCPtr);
  }

  private UIElement GetManipulationContainerHelper() {
    IntPtr cPtr = NoesisGUI_PINVOKE.ManipulationDeltaEventArgs_GetManipulationContainerHelper(swigCPtr);
    return (UIElement)Noesis.Extend.GetProxy(cPtr, false);
  }

}

}

