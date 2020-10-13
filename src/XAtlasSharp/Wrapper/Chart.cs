//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.2
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace XAtlasSharp {

public class Chart : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal Chart(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(Chart obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~Chart() {
    Dispose(false);
  }

  public void Dispose() {
    Dispose(true);
    global::System.GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          XAtlasPINVOKE.delete_Chart(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public int[] Faces {
    set {
      XAtlasPINVOKE.Chart_Faces_set(swigCPtr, ref value[0]);
    } 
        get
        {
            var _temp = new int[FaceCount];
            System.Runtime.InteropServices.Marshal.Copy(XAtlasPINVOKE.Chart_Faces_get(swigCPtr), _temp, 0, (int)FaceCount);
            return _temp;
        }
    
  }

  public uint AtlasIndex {
    set {
      XAtlasPINVOKE.Chart_AtlasIndex_set(swigCPtr, value);
    } 
    get {
      uint ret = XAtlasPINVOKE.Chart_AtlasIndex_get(swigCPtr);
      return ret;
    } 
  }

  public uint FaceCount {
    set {
      XAtlasPINVOKE.Chart_FaceCount_set(swigCPtr, value);
    } 
    get {
      uint ret = XAtlasPINVOKE.Chart_FaceCount_get(swigCPtr);
      return ret;
    } 
  }

  public ChartType Type {
    set {
      XAtlasPINVOKE.Chart_Type_set(swigCPtr, (int)value);
    } 
    get {
      ChartType ret = (ChartType)XAtlasPINVOKE.Chart_Type_get(swigCPtr);
      return ret;
    } 
  }

  public uint Material {
    set {
      XAtlasPINVOKE.Chart_Material_set(swigCPtr, value);
    } 
    get {
      uint ret = XAtlasPINVOKE.Chart_Material_get(swigCPtr);
      return ret;
    } 
  }

}

}
