package main.scala.y2017.Day25

object Runner {
  type TapeValue = Int
  type Result = (Int, TapeValue, String) // Left/right, what to write, next state name
  type State = (String, Map[TapeValue, Result])

  def run(statesList: List[State], initialState: String, runs: Int): Int = {
    val states = statesList.map(state => state._1 -> state).toMap
    val tape = scala.collection.mutable.Map[Int, TapeValue]()

    (1 to runs).foldLeft((0, initialState))((context, step) => {
      val (position, state) = context
      val result = states.get(state).get._2.get(tape.getOrElse(position, 0)).get
      tape.update(position, result._2)
      (position + result._1, result._3)
    })

    tape.values.sum
  }
}
