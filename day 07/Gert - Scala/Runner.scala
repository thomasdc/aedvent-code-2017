package main.scala.y2017.Day07

object Runner {
  def run(input: Iterator[String]): String = {
    val shouts = input.map(parse).toList

    val cpus = shouts.map(_._1).toSet
    val onDisks = shouts.flatMap(_._3).toSet

    cpus.diff(onDisks).head
  }

  def run2(input: Iterator[String]): Int = {
    val shouts = input.map(parse).map(shout => shout._1 -> (shout._2, shout._3.toSet)).toMap
    weigh(level(shouts))
  }

  def weigh(nodes: Map[String, (Int, Int, Set[String])]): Int = {
    val maxLevel = nodes.map(_._2._2).max
    val weighted = scala.collection.mutable.Map[String, Int]()

    for (level <- maxLevel to 0 by -1) {
      val relevantNodes = nodes.filter(_._2._2 == level)
      relevantNodes.foreach(node => {
        val weightedChildren = node._2._3.map(child => child -> weighted.get(child).get)

        if(weightedChildren.nonEmpty) {
          val grouped = weightedChildren.groupBy(_._2)
          if(grouped.size != 1) {
            val different = grouped.filter(_._2.size == 1).head._2.head
            val regularWeight = grouped.filter(_._2.size != 1).keys.head
            val diff = regularWeight - different._2

            return nodes.get(different._1).get._1 + diff
          }
        }
        val weight = node._2._1 + weightedChildren.toList.map(_._2).sum
        weighted += node._1 -> weight
      })
    }

    throw new Exception("Aargh shouldn't come here")
  }

  def level(shouts: Map[String, (Int, Set[String])]): Map[String, (Int, Int, Set[String])] = {
    var currentLevel = 0
    var toMarkNext = List(getRoot(shouts))
    var allLevelledNodes = scala.collection.mutable.MutableList[Map[String, (Int, Int, Set[String])]]()

    while(toMarkNext.nonEmpty) {
      val levelledNodes = shouts.filter(node => toMarkNext.contains(node._1))
        .map(node => node._1 -> (node._2._1, currentLevel, node._2._2))
      toMarkNext = levelledNodes.flatMap(_._2._3).toList
      allLevelledNodes +=  levelledNodes
      currentLevel += 1
    }

    allLevelledNodes.flatten.toMap
  }

  def getRoot(shouts: Map[String, (Int, Set[String])]): String =
    shouts.keySet.diff(shouts.values.flatMap(_._2).toSet).head

  def parse(line: String): (String, Int, Array[String]) = {
    val splitted = line.split(" -> ")

    val disk = splitted.head.split(" ").head
    val weight = splitted.head.split(" ")(1).stripPrefix("(").stripSuffix(")").toInt
    val towers = if(splitted.length != 2) Array[String]() else splitted(1).split(", ")

    (disk, weight, towers)
  }
}
