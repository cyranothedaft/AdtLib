using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using adt.lib.Graphs;
using adt.lib.Graphs.Algorithms;
using NUnit.Framework;



namespace adt.lib.test.Graphs {
   /// <summary>
   /// Runs tests on an 8-vertex 13-edge graph that contains all four types of edges: Tree, Back, Forward, and Cross.
   /// This example comes from the book Introduction to Algorithms, Second Edition, by T. H. Cormen, et al., Figure 22.5 on p. 544.
   /// </summary>
   public class GraphTester_Complex8 : GraphTypeTester {
      protected override Graph<string> GetGraph() {
         var graph = new Graph<string>();
         _s = graph.AddVertex("s");
         _t = graph.AddVertex("t");
         _u = graph.AddVertex("u");
         _v = graph.AddVertex("v");
         _w = graph.AddVertex("w");
         _x = graph.AddVertex("x");
         _y = graph.AddVertex("y");
         _z = graph.AddVertex("z");
         graph.AddEdge(_s, _z);
         graph.AddEdge(_s, _w);
         graph.AddEdge(_t, _v);
         graph.AddEdge(_t, _u);
         graph.AddEdge(_u, _t);
         graph.AddEdge(_u, _v);
         graph.AddEdge(_v, _s);
         graph.AddEdge(_v, _w);
         graph.AddEdge(_w, _x);
         graph.AddEdge(_x, _z);
         graph.AddEdge(_y, _x);
         graph.AddEdge(_z, _y);
         graph.AddEdge(_z, _w);
         return graph;
      }


      private GraphVertex<string> _s, _t, _u, _v, _w, _x, _y, _z;


      #region Graph tests
      [Test]
      public override void Vertexes_Get() {
         var vertexes = TestGraph.Vertexes;
         Assert.IsNotNull(vertexes);

         var array = vertexes.ToArray();
         Assert.AreEqual(8, array.Length);
         Assert.AreSame(_s, array[0]);
         Assert.AreSame(_t, array[1]);
         Assert.AreSame(_u, array[2]);
         Assert.AreSame(_v, array[3]);
         Assert.AreSame(_w, array[4]);
         Assert.AreSame(_x, array[5]);
         Assert.AreSame(_y, array[6]);
         Assert.AreSame(_z, array[7]);
      }


      [Test]
      public override void Edges_Get() {
         TestEdges(TestGraph.Edges,
                   new[] { "s", "z" },
                   new[] { "s", "w" },
                   new[] { "t", "v" },
                   new[] { "t", "u" },
                   new[] { "u", "t" },
                   new[] { "u", "v" },
                   new[] { "v", "s" },
                   new[] { "v", "w" },
                   new[] { "w", "x" },
                   new[] { "x", "z" },
                   new[] { "y", "x" },
                   new[] { "z", "y" },
                   new[] { "z", "w" });
      }


      [Test]
      public override void FindVertex() {
         TestFindVertex("s", _s);
         TestFindVertex("t", _t);
         TestFindVertex("u", _u);
         TestFindVertex("v", _v);
         TestFindVertex("w", _w);
         TestFindVertex("x", _x);
         TestFindVertex("y", _y);
         TestFindVertex("z", _z);
      }


      [Test]
      public override void Neighbors_Edge() {
         TestEdgesIn_ByVertexValue (_s, "v");
         TestEdgesOut_ByVertexValue(_s, "z", "w");

         TestEdgesIn_ByVertexValue (_t, "u");
         TestEdgesOut_ByVertexValue(_t, "v", "u");

         TestEdgesIn_ByVertexValue (_u, "t");
         TestEdgesOut_ByVertexValue(_u, "t", "v");

         TestEdgesIn_ByVertexValue (_v, "t", "u");
         TestEdgesOut_ByVertexValue(_v, "s", "w");

         TestEdgesIn_ByVertexValue (_w, "s", "v", "z");
         TestEdgesOut_ByVertexValue(_w, "x");

         TestEdgesIn_ByVertexValue (_x, "w", "y");
         TestEdgesOut_ByVertexValue(_x, "z");

         TestEdgesIn_ByVertexValue (_y, "z");
         TestEdgesOut_ByVertexValue(_y, "x");

         TestEdgesIn_ByVertexValue (_z, "s", "x");
         TestEdgesOut_ByVertexValue(_z, "y", "w");
      }


      [Test]
      public override void Neighbors_Vertex() {
         TestVertexesIn (_s, _v);
         TestVertexesOut(_s, _z, _w);

         TestVertexesIn (_t, _u);
         TestVertexesOut(_t, _v, _u);

         TestVertexesIn (_u, _t);
         TestVertexesOut(_u, _t, _v);

         TestVertexesIn (_v, _t, _u);
         TestVertexesOut(_v, _s, _w);

         TestVertexesIn (_w, _s, _v, _z);
         TestVertexesOut(_w, _x);

         TestVertexesIn (_x, _w, _y);
         TestVertexesOut(_x, _z);

         TestVertexesIn (_y, _z);
         TestVertexesOut(_y, _x);

         TestVertexesIn (_z, _s, _x);
         TestVertexesOut(_z, _y, _w);
      }

      #endregion Graph tests


      #region Algorithm tests
      [Test]
      public override void DepthFirstSearch_Trace() {
         DepthFirstSearch<string> search = new DepthFirstSearch<string>(TestGraph);
         search.PerformSearch();

         Assert.AreEqual("(s (z (y (x x) y) (w w) z) s) (t (v v) (u u) t)", search.Trace);
      }


      [Test]
      public override void DepthFirstSearch_EdgeTypes() {
         DepthFirstSearch<string> search = new DepthFirstSearch<string>(TestGraph);
         search.PerformSearch();

         TestEdges(search.EdgesOfType(DfsEdgeType.Tree),
                   new[] { "s", "z" },
                   new[] { "z", "y" },
                   new[] { "z", "w" },
                   new[] { "y", "x" },
                   new[] { "t", "v" },
                   new[] { "t", "u" });

         TestEdges(search.EdgesOfType(DfsEdgeType.Back),
                   new[] { "x", "z" },
                   new[] { "u", "t" });

         TestEdges(search.EdgesOfType(DfsEdgeType.Forward),
                   new[] { "s", "w" });

         TestEdges(search.EdgesOfType(DfsEdgeType.Cross),
                   new[] { "w", "x" },
                   new[] { "v", "w" },
                   new[] { "v", "s" },
                   new[] { "u", "v" });
      }


      [Test]
      public override void DepthFirstSearch_Cycles() {
         DepthFirstSearch<string> search = new DepthFirstSearch<string>(TestGraph);
         search.PerformSearch();

         var cycles = search.Cycles.ToArray();
         Assert.AreEqual(2, cycles.Length);

         TestEdges(cycles[0].Edges,
                   new[] { "z", "y" },
                   new[] { "y", "x" },
                   new[] { "x", "z" });
         TestEdges(cycles[1].Edges,
                   new[] { "t", "u" },
                   new[] { "u", "t" });
      }
      #endregion Algorithm tests
   }
}
