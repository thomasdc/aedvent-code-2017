package main.scala.y2017.Day11

object Runner {
  def run(input: String): Int =
    distance(parse(input).foldLeft((0, 0, 0)){(pos, direction) => move(pos, direction)})

  def run2(input: String): Int = {
    parse(input).foldLeft(((0, 0, 0), 0)){ (params, direction) =>
      val newPos = move(params._1, direction)
      (newPos, Math.max(distance(newPos), params._2))
    }._2
  }

  def parse(input: String): Array[String] = input.split(",").map(_.trim)

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

  def distance(pos: (Int, Int, Int)): Int = List(Math.abs(pos._1), Math.abs(pos._2), Math.abs(pos._3)).max
}