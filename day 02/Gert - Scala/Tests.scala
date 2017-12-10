package main.scala.y2017.Day02

import main.scala.TestBase
import scala.io.Source

class Tests extends TestBase {
  test("Example 1") {
    Runner.run(Array(Array(5, 1, 9, 5), Array(7, 5, 3), Array(2, 4, 6, 8))) shouldBe 18
  }

  test("Puzzle 1") {
    val input = Source.fromFile("test/main/scala/y2017/Day02/puzzle.txt").getLines()
    Runner.run(input.toArray.map(_.split(" ").map(_.toInt))) shouldBe 32020
  }

  test("Example 2") {
    Runner.run2(Array(Array(5, 9, 2, 8), Array(9, 4, 7, 3), Array(3, 8, 6, 5))) shouldBe 9
  }

  test("Puzzle 2") {
    val input = Source.fromFile("test/main/scala/y2017/Day02/puzzle.txt").getLines()
    Runner.run2(input.toArray.map(_.split(" ").map(_.toInt))) shouldBe 236
  }
}
