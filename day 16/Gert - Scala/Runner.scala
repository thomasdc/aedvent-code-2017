package main.scala.y2017.Day16

class Runner {
  val formerDances = scala.collection.mutable.ArrayBuffer[(String, String)]()

  def run(danceMoves: Array[String], repetitions: Int = 1, inputSize: Int = 16): String = {
    var position = startPosition(inputSize)
    val moves = danceMoves.map(parse)

    (1 to repetitions).foreach(repetition => {
      if(formerDances.map(_._1).contains(position))
        return formerDances(repetitions % formerDances.size)._1
      else {
        val newPosition = moves.foldLeft(position) { (position, move) => move.act(position) }
        formerDances.append((position, newPosition))
        position = newPosition
      }
    })

    position
  }

  def startPosition(inputSize: Int): String =
    (0 until inputSize).map(pos => (97 + pos).toChar).mkString("")

  def parse(move: String): Move = {
    move.charAt(0) match {
      case 's' => new Spin(move.drop(1).toInt)
      case 'x' =>
        val params = move.drop(1).split("/")
        new Exchange(Integer.parseInt(params(0)), Integer.parseInt(params(1)))
      case 'p' =>
        val params = move.drop(1).split("/")
        new Partner(params(0).charAt(0), params(1).charAt(0))
    }
  }
}
