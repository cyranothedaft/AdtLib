using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using adt.lib.Graphs;
using NUnit.Framework;



namespace adt.lib.test.Graphs {
   [TestFixture]
   public abstract class GraphTypeTester {
      protected Graph<string> TestGraph { get; private set; }


      [SetUp]
      public void ConstructGraph() {
         TestGraph = GetGraph();
      }

      protected abstract Graph<string> GetGraph();


      #region Graph tests
      [Test] public abstract void Vertexes_Get();
      [Test] public abstract void Edges_Get();
      [Test] public abstract void FindVertex();
      [Test] public abstract void Neighbors_Edge();
      [Test] public abstract void Neighbors_Vertex();
      #endregion Graph tests


      #region Algorithm tests
      [Test] public abstract void DepthFirstSearch_Trace();
      [Test] public abstract void DepthFirstSearch_EdgeTypes();
      [Test] public abstract void DepthFirstSearch_Cycles();
      #endregion


      #region Test helpers for subclasses
      protected void TestFindVertex(string searchValue, GraphVertex<string> vertex) {
         var found = TestGraph.FindVertex(searchValue, string.Equals);
         Assert.IsNotNull(found);
         Assert.AreEqual(searchValue, found.Value);
      }


      // test edges by instance

      protected void TestEdgesIn_ByInstance(GraphVertex<string> vertex, params GraphEdge<string>[] inEdgeInstances) {
         testEdgeInstances(vertex.EdgesIn, inEdgeInstances);
      }

      protected void TestEdgesOut_ByInstance(GraphVertex<string> vertex, params GraphEdge<string>[] outEdgeInstances) {
         testEdgeInstances(vertex.EdgesOut, outEdgeInstances);
      }

      private void testEdgeInstances(IEnumerable<GraphEdge<string>> edges, GraphEdge<string>[] expectedEdges) {
         Assert.IsNotNull(edges);

         var array = edges.ToArray();
         Assert.AreEqual(expectedEdges.Length, array.Length);
         for ( int i = 0; i < array.Length; ++i )
            Assert.AreSame(expectedEdges[i], array[i], "edge #" + i);
      }

      // test edges by from/to vertex values

      protected void TestEdgesIn_ByVertexValue(GraphVertex<string> vertex, params string[] edgeFromVertexValues) {
         testVertexesByEdge(vertex.EdgesIn, e => e.From, edgeFromVertexValues);
      }

      protected void TestEdgesOut_ByVertexValue(GraphVertex<string> vertex, params string[] edgeToVertexValues) {
         testVertexesByEdge(vertex.EdgesOut, e => e.To, edgeToVertexValues);
      }

      private void testVertexesByEdge(IEnumerable<GraphEdge<string>> edges, Func<GraphEdge<string>, GraphVertex<string>> edgeVertexSelector, string[] expectedVertexValues) {
         Assert.IsNotNull(edges);

         var array = edges.ToArray();
         Assert.AreEqual(expectedVertexValues.Length, array.Length);
         for ( int i = 0; i < array.Length; ++i )
            Assert.AreEqual(expectedVertexValues[i], edgeVertexSelector(array[i]).Value, "vertex #" + i);
      }


      // test vertexes by instance

      protected void TestVertexesIn(GraphVertex<string> vertex, params GraphVertex<string>[] fromVertexInstances) {
         testVertexInstances(vertex.VertexesIn, fromVertexInstances);
      }

      protected void TestVertexesOut(GraphVertex<string> vertex, params GraphVertex<string>[] toVertexInstances) {
         testVertexInstances(vertex.VertexesOut, toVertexInstances);
      }

      private void testVertexInstances(IEnumerable<GraphVertex<string>> vertexes, GraphVertex<string>[] expectedVertexes) {
         Assert.IsNotNull(vertexes);

         var array = vertexes.ToArray();
         Assert.AreEqual(expectedVertexes.Length, array.Length);
         for ( int i = 0; i < array.Length; ++i )
            Assert.AreSame(expectedVertexes[i], array[i], "vertex #" + i);
      }


      // test edges by from/to vertex values

      protected void TestEdges(IEnumerable<GraphEdge<string>> edges, params string[][] vertexValuePairs) {
         Assert.IsNotNull(edges);
         var array = edges.ToArray();
         Assert.AreEqual(vertexValuePairs.Length, array.Length);

         GraphEdge<string>[] actualEdgesOrdered = edges.OrderBy(e => e.From.Value)
                                                         .ThenBy(e => e.To.Value)
                                                         .ToArray();
         string[][] expectedEdgesOrdered = vertexValuePairs.OrderBy(p => p[0])
                                                         .ThenBy(p => p[1])
                                                         .ToArray();

         for ( int i = 0; i < array.Length; ++i ) {
            Assert.AreEqual(expectedEdgesOrdered[i][0], actualEdgesOrdered[i].From.Value, string.Format("edge #{0} :From", i));
            Assert.AreEqual(expectedEdgesOrdered[i][1], actualEdgesOrdered[i].To.Value, string.Format("edge #{0} :To", i));
         }
      }

      #endregion Test helpers for subclasses
   }
}
