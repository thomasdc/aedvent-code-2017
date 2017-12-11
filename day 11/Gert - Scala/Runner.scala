package main.scala.y2017.Day11

object Runner {
  type Log = Map[String, Int]

  def run(input: String): Int = reduce(toLog(input)).values.sum

  def run2(input: String): Int = {
    (1 to input.length).map(sliceLength => {
      val d = reduce(toLog(input.take(sliceLength))).values.sum
      if(d == 1623) {
        println(toLog(input.take(sliceLength)))
        println(reduce(toLog(input.take(sliceLength))))
      }
      d
    }).max
  }

  def runB(input: String): Int = {
    var pos = (0, 0, 0)
    var maxDistance = 0
    parse(input).foreach(direction => {
      pos = move(pos, direction)
      maxDistance = Math.max(maxDistance, distance(pos))
      if(distance(pos) == 1622)
        println(pos)
    })

    maxDistance
  }

  def toLog(input: String): Log = {
    fill(parse(input).groupBy(direction => direction)
      .map(direction => direction._1 -> direction._2.length))
  }

  def reduce(log: Log): Log = {
    reduceSchweeps(reduceZigZags(reduceOpposites(log)))
  }

  def reduceOpposites(log: Log): Log = {
    reducePair(reducePair(reducePair(log, "n", "s"), "ne", "sw"), "se", "nw")
  }

  def reduceZigZags(log: Log): Log = {
    reducePairInto(reducePairInto(log, "sw", "se", "s"), "nw", "ne", "n")
  }

  def reduceSchweeps(log: Log): Log = {
    reducePairInto(reducePairInto(reducePairInto(reducePairInto(log,
      "ne", "s", "se"), "nw", "s", "sw"), "sw", "n", "nw"), "se", "n", "ne")
  }

  def reducePair(log: Log, a: String, b: String): Log = {
    log.updated(a, Math.max(log(a) - log(b), 0))
      .updated(b, Math.max(log(b) - log(a), 0))
  }

  def reducePairInto(log: Log, a: String, b: String, into: String): Log = {
    val amount = Math.min(log(a), log(b))
    log.updated(a, Math.max(log(a) - amount, 0))
      .updated(b, Math.max(log(b) - amount, 0))
      .updated(into, log(into) + amount)
  }

//  def run(input: String): Int = {
//    val finalPos = parse(input).foldLeft((0, 0, 0)){ (pos, direction) =>
//      move(pos, direction)
//    }
//
//    distance(finalPos)
//  }

  def parse(input: String): Array[String] = input.split(",").map(_.trim)

  def fill(log: Log): Log = {
    log.updated("n", log.getOrElse("n", 0)).updated("s", log.getOrElse("s", 0))
      .updated("ne", log.getOrElse("ne", 0)).updated("se", log.getOrElse("se", 0))
      .updated("nw", log.getOrElse("nw", 0)).updated("sw", log.getOrElse("sw", 0))
  }

  def move(pos: (Int, Int, Int), direction: String): (Int, Int, Int) = {
    direction match {
      case "n" => (pos._1, pos._2 + 1, pos._3 - 1)
      case "nw" => (pos._1 - 1, pos._2 + 1, pos._3)
      case "ne" => (pos._1 + 1, pos._2, pos._3 - 1)
      case "s" => (pos._1, pos._2 - 1, pos._3 + 1)
      case "sw" => (pos._1 - 1, pos._2, pos._3 + 1)
      case "se" => (pos._1 + 1, pos._2 - 1, pos._3)
      case _ => throw new Exception("Direction %s not supported".format(direction))
    }
  }

  def distance(pos: (Int, Int, Int)): Int = {
    List(Math.abs(pos._1),
      Math.abs(pos._2),
      Math.abs(pos._3)).max
  }
}