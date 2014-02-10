using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adt.lib.Graphs {
   public partial class GraphNode<T> {
      public GraphNode(Graph<T> parentGraph, T value) {
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

      internal readonly List<GraphNode<T>> _in = new List<GraphNode<T>>(); 
      internal readonly List<GraphNode<T>> _out = new List<GraphNode<T>>();

      internal readonly List<GraphEdge<T>> _inEdges = new List<GraphEdge<T>>();
      internal readonly List<GraphEdge<T>> _outEdges = new List<GraphEdge<T>>();


      /// <summary>
      /// The list of all vertexes connected to this one by inbound edges.
      /// </summary>
      public IEnumerable<GraphNode<T>> VertexesIn {
         get { return _in; }
      }

      /// <summary>
      /// The list of all vertexes connected to this one by outbound edges.
      /// </summary>
      public IEnumerable<GraphNode<T>> VertexesOut {
         get { return _out; }
      }

      public IEnumerable<GraphEdge<T>> EdgesIn {
         get { return _inEdges; }
      }

      public IEnumerable<GraphEdge<T>> EdgesOut {
         get { return _outEdges; }
      }


      public GraphEdge<T> AddEdgeTo(GraphNode<T> v) {
         return _parentGraph.AddEdge(this, v);
      }
   }
}
