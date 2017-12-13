package main.scala.y2017.Day12

object Runner {
  type Edge = (String, String)

  def run(input: String): Int = run(input.split("\r\n"))

  def run2(input: String): Int = run2(input.split("\r\n"))

  def run(input: Array[String]): Int = connectedTo("0", input.flatMap(parse)).length

  def run2(input: Array[String]): Int = groups(input.flatMap(parse))

  def connectedTo(node: String, edges: Array[Edge]): List[String] = {
    var allNodes = scala.collection.mutable.ListBuffer(node)
    var nodesToConsider = allNodes

    while(nodesToConsider.nonEmpty) {
      val extraNodes = nodesToConsider.flatMap(node =>
        edges.filter(edge => edge._1 == node).map(edge => edge._2).toSet
      ).filter(node => !allNodes.contains(node))

      extraNodes.foreach(node => allNodes += node)
      nodesToConsider = extraNodes
    }

    allNodes.toList
  }

  def groups(allEdges: Array[Edge]): Int = {
    var edges = allEdges
    var groups = 0

    while(edges.nonEmpty) {
      groups += 1
      edges = eliminateEdges(connectedTo(edges.head._1, edges), edges)
    }

    groups
  }

  def eliminateEdges(nodes: List[String], edges: Array[Edge]): Array[Edge] =
    edges.filter(edge => !nodes.contains(edge._1))

  def parse(entry: String): Array[Edge] = {
    val split = entry.split(" <-> ")
    split(1).split(", ").flatMap(to => Array((split.head, to), (to, split.head)))
  }
}
