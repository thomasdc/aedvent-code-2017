package main.scala.y2017.Day20

import main.scala.TestBase
import scala.io.Source

class Tests extends TestBase {
  test("Example 1") {
    Runner.run(Array(
      "p=< 3,0,0>, v=< 2,0,0>, a=<-1,0,0>",
      "p=< 4,0,0>, v=< 0,0,0>, a=<-2,0,0>"
    )) shouldBe 0
  }

  test("Puzzle 1") {
    val input = Source.fromFile("test/main/scala/y2017/Day20/puzzle.txt").getLines().toArray
    Runner.run(input) shouldBe 258
  }

  test("Example 2") {
    Runner.run2(Array(
      "p=<-6,0,0>, v=< 3,0,0>, a=< 0,0,0>",
      "p=<-4,0,0>, v=< 2,0,0>, a=< 0,0,0>",
      "p=<-2,0,0>, v=< 1,0,0>, a=< 0,0,0>",
      "p=< 3,0,0>, v=<-1,0,0>, a=< 0,0,0>"
    )) shouldBe 4
  }

  test("Puzzle 2") {
    val input = Source.fromFile("test/main/scala/y2017/Day20/puzzle.txt").getLines().toArray
    Runner.run2(input) shouldBe 707
  }
}
