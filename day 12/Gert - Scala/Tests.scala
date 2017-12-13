package main.scala.y2017.Day12

import main.scala.TestBase

import scala.io.Source

class Tests extends TestBase {
  test("Example 1") {
    Runner.run(
      """0 <-> 2
        |1 <-> 1
        |2 <-> 0, 3, 4
        |3 <-> 2, 4
        |4 <-> 2, 3, 6
        |5 <-> 6
        |6 <-> 4, 5""".stripMargin) shouldBe 6
  }

  test("Puzzle 1") {
    val input = Source.fromFile("test/main/scala/y2017/Day12/puzzle.txt").getLines().mkString("\r\n")
    Runner.run(input) shouldBe 175
  }

  test("Example 2") {
    Runner.run2(
      """0 <-> 2
        |1 <-> 1
        |2 <-> 0, 3, 4
        |3 <-> 2, 4
        |4 <-> 2, 3, 6
        |5 <-> 6
        |6 <-> 4, 5""".stripMargin) shouldBe 2
  }

  test("Puzzle 2") {
    val input = Source.fromFile("test/main/scala/y2017/Day12/puzzle.txt").getLines().mkString("\r\n")
    Runner.run2(input) shouldBe 213
  }
}
