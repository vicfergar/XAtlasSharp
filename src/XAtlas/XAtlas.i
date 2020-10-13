/* File XAtlas.i */
 
%module XAtlas

%include "typemaps.i"

%include "arrays_csharp.i"
%include "cs_struct.i"
%include "stdint.i"
%include "carrays.i"
%include "swigtype_inout.i"

%rename("%(camelcase)s") "";
%rename(Faces) faceArray;
%rename(Charts) chartArray;
%rename(Indices) indexArray;
%rename(Vertices) vertexArray;

%define MAP_TO_INTPTR(cType)
    %typemap(cstype) cType* "System.IntPtr"
    %typemap(csin) cType* "new System.Runtime.InteropServices.HandleRef(null, $csinput)"
    %typemap(csout, excode=SWIGEXCODE) cType* "{System.IntPtr res = $imcall; $excode; return res;}"
    %typemap(csvarout, excode=SWIGEXCODE2) cType* "get{System.IntPtr res = $imcall; $excode; return res;}"
    %typemap(csdirectorin) cType* "$iminput"
    %typemap(csdirectorout) cType* "$cscall"
%enddef

// this will make the IntPtr thing work
MAP_TO_INTPTR(void)
MAP_TO_INTPTR(uint32_t)
MAP_TO_INTPTR(bool)
MAP_TO_INTPTR(uint8_t)

%typemap(ctype) float[ANY]   "float *"
%typemap(imtype, out="System.IntPtr") float[ANY]   "ref float"
%typemap(cstype) float[ANY]   "float[]"
%typemap(csin) float[ANY] "ref $csinput[0]"
%typemap(csvarout, excode=SWIGEXCODE2) float[ANY]
%{
    get
    {
        var _temp = new float[$dim0];
        System.Runtime.InteropServices.Marshal.Copy($imcall, _temp, 0, $dim0);
        return _temp;
    }
%}

%define MAP_TO_ARRAY(cType, csType, property, count)
    %typemap(cstype) cType* property "csType[]"
    %typemap(imtype, out="System.IntPtr") cType* property   "ref csType"
    %typemap(csin) cType* property "ref $csinput[0]"
    %typemap(csvarout, excode=SWIGEXCODE2) cType* property
    %{
        get
        {
            var _temp = new csType[count];
            System.Runtime.InteropServices.Marshal.Copy($imcall, _temp, 0, (int)count);
            return _temp;
        }
    %}
%enddef

MAP_TO_ARRAY(uint32_t, int, faceArray, FaceCount)
MAP_TO_ARRAY(uint32_t, int, indexArray, IndexCount)
MAP_TO_ARRAY(float, float, utilization, AtlasCount)

%define MAP_TO_IENUM(property, cType, csType, sizeFunc, count)
  %immutable property;
  %typemap(cstype) cType* "System.Collections.Generic.IEnumerable<csType>"
  %typemap(csvarout, excode=SWIGEXCODE2) cType*
  %{
      get
      {
          var cPtr = $imcall;
          var size = sizeFunc();
          for (int i = 0; i < count; i++)
          {
              yield return new csType(System.IntPtr.Add(cPtr, size * i), false);
          }
      }
  %}
%enddef

MAP_TO_IENUM(xatlas::Mesh::chartArray, xatlas::Chart, Chart, XAtlasPINVOKE.GetChartSize, ChartCount)
MAP_TO_IENUM(xatlas::Mesh::vertexArray, xatlas::Vertex, Vertex, XAtlasPINVOKE.GetVertexSize, VertexCount)
MAP_TO_IENUM(xatlas::Atlas::meshes, xatlas::Mesh, Mesh, XAtlasPINVOKE.GetMeshSize, MeshCount)

%insert(wrapper)
%{
/* Helper methods to get struct sizes */
SWIGEXPORT size_t SWIGSTDCALL GetChartSize()
{
    return sizeof(xatlas::Chart);
}
SWIGEXPORT size_t SWIGSTDCALL GetVertexSize()
{
    return sizeof(xatlas::Vertex);
}
SWIGEXPORT size_t SWIGSTDCALL GetMeshSize()
{
    return sizeof(xatlas::Mesh);
}
%}

extern int GetChartSize();
extern int GetVertexSize();
extern int GetMeshSize();

%typemap(cscode) xatlas::ChartOptions
%{
  public delegate void ParameterizeFuncDelegate(System.IntPtr positions, System.IntPtr texcoords, System.IntPtr vertexCount, System.IntPtr indices, uint indexCount);
%}

%pragma(csharp) modulecode=%{
  public delegate bool ProgressFuncDelegate(ProgressCategory category, int progress, System.IntPtr userData);
  public delegate System.IntPtr ReallocFuncDelegate(System.IntPtr ptr, ulong size);
  public delegate void FreeFuncDelegate(System.IntPtr ptr);
  public delegate int PrintFuncDelegate(string message);
%}

%define %cs_callback(TYPE, CSTYPE)
    %typemap(ctype) TYPE, TYPE& "void*"
    %typemap(in) TYPE  %{ $1 = ($1_type)$input; %}
    %typemap(in) TYPE& %{ $1 = ($1_type)&$input; %}
    %typemap(imtype) TYPE, TYPE& "CSTYPE"
    %typemap(cstype) TYPE, TYPE& "CSTYPE"
    %typemap(csin) TYPE, TYPE& "$csinput"
%enddef

%cs_callback(xatlas::ProgressFunc, XAtlas.ProgressFuncDelegate)
%cs_callback(xatlas::ReallocFunc, XAtlas.ReallocFuncDelegate)
%cs_callback(xatlas::FreeFunc, XAtlas.FreeFuncDelegate)
//%cs_callback(xatlas::PrintFunc, XAtlas.PrintFuncDelegate)

%cs_callback(xatlas::ParameterizeFunc, ChartOptions.ParameterizeFuncDelegate)
%typemap(cstype) xatlas::ParameterizeFunc "ParameterizeFuncDelegate"
%typemap(csvarout) xatlas::ParameterizeFunc "get => $imcall;"


%insert(header)
%{
#include <cstdarg>

/* Helper callback to solve PInvoke with va_list */
typedef int (*xatlasPrintFuncHelper)(const char *);
static xatlasPrintFuncHelper XAtlas_PrintFuncHelper_callback = NULL;
static int XAtlas_PrintFuncHelper(const char *format, ...)
{
    if (XAtlas_PrintFuncHelper_callback)
    {
        char buffer[256];
        va_list args;
        va_start (args, format);
        vsprintf (buffer,format, args);
        va_end (args);
        return XAtlas_PrintFuncHelper_callback((const char *)SWIG_csharp_string_callback(buffer));
    }

    return 0;
}
%}
%typemap(ctype) xatlas::PrintFunc "void*"
%typemap(cstype) xatlas::PrintFunc "XAtlas.PrintFuncDelegate"
%typemap(imtype) xatlas::PrintFunc "XAtlas.PrintFuncDelegate"
%typemap(csin) xatlas::PrintFunc "$csinput"
%typemap(in) xatlas::PrintFunc %{ XAtlas_PrintFuncHelper_callback = (xatlasPrintFuncHelper)$input; $1 = ($1_type)XAtlas_PrintFuncHelper; %}

%csmethodmodifiers xatlas::Create "internal";
%csmethodmodifiers xatlas::Destroy "internal";
%csmethodmodifiers xatlas::AddMesh "internal";
%csmethodmodifiers xatlas::AddMeshJoin "internal";
%csmethodmodifiers xatlas::AddUvMesh "internal";
%csmethodmodifiers xatlas::ComputeCharts "internal";
%csmethodmodifiers xatlas::PackCharts "internal";
%csmethodmodifiers xatlas::Generate "internal";
%csmethodmodifiers xatlas::SetProgressCallback "internal";

%nodefaultctor Atlas;
%nodefaultctor Chart;
%nodefaultctor Mesh;
%nodefaultctor Vertex;

%typemap(cscode) xatlas::Atlas
%{
  public static Atlas Create() => XAtlas.Create();
  public void Destroy() => XAtlas.Destroy(this);
  public AddMeshError AddMesh(MeshDecl meshDecl) => XAtlas.AddMesh(this, meshDecl);
  public AddMeshError AddMesh(MeshDecl meshDecl, uint meshCountHint) => XAtlas.AddMesh(this, meshDecl, meshCountHint);
  public void AddMeshJoin() => XAtlas.AddMeshJoin(this);
  public void AddUvMesh(UvMeshDecl decl) => XAtlas.AddUvMesh(this, decl);
  public void ComputeCharts() => XAtlas.ComputeCharts(this);
  public void ComputeCharts(ChartOptions options) => XAtlas.ComputeCharts(this, options);
  public void PackCharts() => XAtlas.PackCharts(this);
  public void PackCharts(PackOptions packOptions) => XAtlas.PackCharts(this, packOptions);
  public void Generate() => XAtlas.Generate(this);
  public void Generate(ChartOptions chartOptions) => XAtlas.Generate(this, chartOptions);
  public void Generate(ChartOptions chartOptions, PackOptions packOptions) => XAtlas.Generate(this, chartOptions, packOptions);
  public void SetProgressCallback(XAtlas.ProgressFuncDelegate progressFunc) => XAtlas.SetProgressCallback(this, progressFunc);
  public void SetProgressCallback(XAtlas.ProgressFuncDelegate progressFunc, System.IntPtr progressUserData) => XAtlas.SetProgressCallback(this, progressFunc, progressUserData);
%}

%{
#include "xatlas.h"
%}

%include "xatlas.h"
