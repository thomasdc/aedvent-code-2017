package main.scala.y2017.Day05

import main.scala.TestBase
import scala.io.Source

class Tests extends TestBase {
  test("Example 1") { Runner.run(Array(0, 3, 0, 1, -3)) shouldBe 5 }

  test("Puzzle 1") {
    val input = Source.fromFile("test/main/scala/y2017/Day05/puzzle.txt").getLines()
    Runner.run(input.next().split(", ").map(_.toInt)) shouldBe 342669
  }

  test("Example 2") { Runner.run(Array(0, 3, 0, 1, -3), true) shouldBe 10 }

  test("Puzzle 2") {
    val input = Source.fromFile("test/main/scala/y2017/Day05/puzzle.txt").getLines()
    Runner.run(input.next().split(", ").map(_.toInt), true) shouldBe 25136209
  }
}
