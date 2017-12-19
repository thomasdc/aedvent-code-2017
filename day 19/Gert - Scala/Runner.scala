package main.scala.y2017.Day19

object Runner {
  type Maze = Array[Array[Char]]

  def run(input: String): String = new Walker(parse(input)).solve()

  def parse(input: String): Maze = {
    val rowLength = input.split("\r\n").map(_.length).max
    input.split("\r\n").map(row =>
      (row + (0 to (rowLength - row.length) + 1).map(_ => ' ').mkString("")).toCharArray
    )
  }

  class Walker(maze: Maze) {
    def solve(): String = {
      var pos = (0, maze(0).indexOf('|'))
      var direction = (1, 0)
      val bases = scala.collection.mutable.ArrayBuffer[String]()
      var steps = 0

      do {
        pos = (pos._1 + direction._1, pos._2 + direction._2)
        direction = determineDirection(pos, direction)

        if(maze(pos._1)(pos._2).isLetter)
          bases.append(maze(pos._1)(pos._2).toString)

        steps += 1
      } while(maze(pos._1)(pos._2) != ' ')

      bases.mkString("")
    }

    def determineDirection(pos: (Int, Int), direction: (Int, Int)): (Int, Int) = {
      maze(pos._1)(pos._2) match {
        case '+' =>
          if(check((pos._1, pos._2 + 1)) && direction != (0, -1)) (0, 1)
          else if(check((pos._1 + 1, pos._2)) && direction != (-1, 0)) (1, 0)
          else if(check((pos._1, pos._2 - 1)) && direction != (0, 1)) (0, -1)
          else if(check((pos._1 - 1, pos._2)) && direction != (1, 0)) (-1, 0)
          else direction
        case _ => direction
      }
    }

    def check(pos: (Int, Int)): Boolean = {
      pos._1 < maze.length && pos._2 < maze(pos._1).length &&
        pos._1 >= 0 && pos._2 >= 0 &&
        maze(pos._1)(pos._2) != ' '
    }
  }
}
