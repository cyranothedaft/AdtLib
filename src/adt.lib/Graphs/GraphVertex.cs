using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace adt.lib.Graphs {
   [DebuggerDisplay("{Value}")]
   public partial class GraphVertex<T> {
      public GraphVertex(Graph<T> parentGraph, T value) {
         _parentGraph = parentGraph;
         Value = value;
      }


      private readonly Graph<T> _parentGraph;

      /// <summary>
      /// The value wrapped or stored by this graph vertex.
      /// </summary>
      public T Value { get; private set; }

      /// <summary>
      /// List of all vertexes connected to this one by in/outbound edges.
      /// </summary>

      internal readonly List<GraphVertex<T>> _in = new List<GraphVertex<T>>(); 
      internal readonly List<GraphVertex<T>> _out = new List<GraphVertex<T>>();

      internal readonly List<GraphEdge<T>> _inEdges = new List<GraphEdge<T>>();
      internal readonly List<GraphEdge<T>> _outEdges = new List<GraphEdge<T>>();


      /// <summary>
      /// The list of all vertexes connected to this one by inbound edges.
      /// </summary>
      public IEnumerable<GraphVertex<T>> VertexesIn {
         get { return _in; }
         //get { return _in.OrderBy(v => v.Value); }
      }

      /// <summary>
      /// The list of all vertexes connected to this one by outbound edges.
      /// </summary>
      public IEnumerable<GraphVertex<T>> VertexesOut {
         get { return _out; }
         //get { return _out.OrderBy(v => v.Value); }
      }

      public IEnumerable<GraphEdge<T>> EdgesIn {
         get {
            return _inEdges;
            //return _inEdges.OrderBy(e => e.From.Value)
            //               .ThenBy(e => e.To.Value);
         }
      }

      public IEnumerable<GraphEdge<T>> EdgesOut {
         get {
            return _outEdges;
            //return _outEdges.OrderBy(e => e.From.Value)
            //                .ThenBy(e => e.To.Value);
         }
      }


      //public GraphEdge<T> AddEdgeTo(GraphVertex<T> v) {
      //   return _parentGraph.AddEdge(this, v);
      //}
   }
}
