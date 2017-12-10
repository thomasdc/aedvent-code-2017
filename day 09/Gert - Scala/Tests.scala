package main.scala.y2017.Day09

import main.scala.TestBase
import scala.io.Source

class Tests extends TestBase {
  test("Example 1") { Runner.run("{{{}}}") shouldBe 6 }
  test("Example 2") { Runner.run("{{},{}}") shouldBe 5 }
  test("Example 3") { Runner.run("{{{},{},{{}}}}") shouldBe 16 }
  test("Example 4") { Runner.run("{<a>,<a>,<a>,<a>}") shouldBe 1 }
  test("Example 5") { Runner.run("{{<ab>},{<ab>},{<ab>},{<ab>}}") shouldBe 9 }
  test("Example 6") { Runner.run("{{<!!>},{<!!>},{<!!>},{<!!>}}") shouldBe 9 }
  test("Example 7") { Runner.run("{{<a!>},{<a!>},{<a!>},{<ab>}}") shouldBe 3 }

  test("Puzzle 1") {
    val input = Source.fromFile("test/main/scala/y2017/Day09/puzzle.txt").getLines().mkString("")
    Runner.run(input) shouldBe 11347
  }

  test("Example 2.1") { Runner.run("<>", true) shouldBe 0 }
  test("Example 2.2") { Runner.run("<random characters>", true) shouldBe 17 }
  test("Example 2.3") { Runner.run("<<<<>", true) shouldBe 3 }
  test("Example 2.4") { Runner.run("<{!>}>", true) shouldBe 2 }
  test("Example 2.5") { Runner.run("<!!>", true) shouldBe 0 }
  test("Example 2.6") { Runner.run("<!!!>>", true) shouldBe 0 }
  test("Example 2.7") { Runner.run("<{o\"i!a,<{i<a>", true) shouldBe 10 }
  test("Example 2.8") { Runner.run("<>x", true) shouldBe 0 }

  test("Puzzle 2") {
    val input = Source.fromFile("test/main/scala/y2017/Day09/puzzle.txt").getLines().mkString("")
    Runner.run(input, true) shouldBe 5404
  }
}
