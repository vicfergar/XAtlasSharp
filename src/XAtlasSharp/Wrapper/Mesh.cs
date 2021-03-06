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

public class Mesh : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal Mesh(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(Mesh obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~Mesh() {
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
          XAtlasPINVOKE.delete_Mesh(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public System.Collections.Generic.IEnumerable<Chart> Charts {
      get
      {
          var cPtr = XAtlasPINVOKE.Mesh_Charts_get(swigCPtr);
          var size = XAtlasPINVOKE.GetChartSize();
          for (int i = 0; i < ChartCount; i++)
          {
              yield return new Chart(System.IntPtr.Add(cPtr, size * i), false);
          }
      }
  
  }

  public int[] Indices {
    set {
      XAtlasPINVOKE.Mesh_Indices_set(swigCPtr, ref value[0]);
    } 
        get
        {
            var _temp = new int[IndexCount];
            System.Runtime.InteropServices.Marshal.Copy(XAtlasPINVOKE.Mesh_Indices_get(swigCPtr), _temp, 0, (int)IndexCount);
            return _temp;
        }
    
  }

  public System.Collections.Generic.IEnumerable<Vertex> Vertices {
      get
      {
          var cPtr = XAtlasPINVOKE.Mesh_Vertices_get(swigCPtr);
          var size = XAtlasPINVOKE.GetVertexSize();
          for (int i = 0; i < VertexCount; i++)
          {
              yield return new Vertex(System.IntPtr.Add(cPtr, size * i), false);
          }
      }
  
  }

  public uint ChartCount {
    set {
      XAtlasPINVOKE.Mesh_ChartCount_set(swigCPtr, value);
    } 
    get {
      uint ret = XAtlasPINVOKE.Mesh_ChartCount_get(swigCPtr);
      return ret;
    } 
  }

  public uint IndexCount {
    set {
      XAtlasPINVOKE.Mesh_IndexCount_set(swigCPtr, value);
    } 
    get {
      uint ret = XAtlasPINVOKE.Mesh_IndexCount_get(swigCPtr);
      return ret;
    } 
  }

  public uint VertexCount {
    set {
      XAtlasPINVOKE.Mesh_VertexCount_set(swigCPtr, value);
    } 
    get {
      uint ret = XAtlasPINVOKE.Mesh_VertexCount_get(swigCPtr);
      return ret;
    } 
  }

}

}
