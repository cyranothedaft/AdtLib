using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adt.lib.Graphs {
   /// <summary>
   /// Represents an unweighted, directed graph.  Uses adjacency list for internal representation, so this class is not optimal for storing a dense graph.
   /// </summary>
   /// <typeparam name="T"></typeparam>
   public partial class Graph<T> {
      /// <summary>
      /// The list of graph vertexes.  Each vertex maintains a list of its in- and out-bound adjacent vertexes.
      /// </summary>
      private readonly List<GraphNode<T>> _vertexes = new List<GraphNode<T>>();


      /// <summary>
      /// The list of graph edges.
      /// </summary>
      private readonly List<GraphEdge<T>> _edges = new List<GraphEdge<T>>();


      public IEnumerable<GraphNode<T>> Vertexes { get { return _vertexes; } }
      public IEnumerable<GraphEdge<T>> Edges { get { return _edges; } }


      public GraphNode<T> AddVertex(T value) {
         var v = new GraphNode<T>(this, value);
         _vertexes.Add(v);
         return v;
      }


      public GraphEdge<T> AddEdge(GraphNode<T> v1, GraphNode<T> v2) {
         v1._out.Add(v2);
         v2._in.Add(v1);

         var edge = new GraphEdge<T>(v1, v2);
         _edges.Add(edge);
         v1._outEdges.Add(edge);
         v2._inEdges.Add(edge);
         return edge;
      }


      /// <summary>
      /// Searches the list of vertexes for one containing the given vlue.
      /// Returns the first match found.
      /// Returns null if no match is found.
      /// </summary>
      /// <param name="value"></param>
      /// <param name="matches"></param>
      /// <returns></returns>
      public GraphNode<T> FindVertex(T value, Func<T, T, bool> matches) {
         return _vertexes.FirstOrDefault(v => matches(v.Value, value));
      }
   }
}
