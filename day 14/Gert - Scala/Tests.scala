package main.scala.y2017.Day14

import main.scala.TestBase

class Tests extends TestBase {
  test("Example 1") { Runner.run("flqrgnkx") shouldBe 8108 }
  test("Puzzle 1") { Runner.run("ljoxqyyw") shouldBe 8316 }
  test("Example 2") { Runner.run2("flqrgnkx") shouldBe 1242 }
  test("Puzzle 2") { Runner.run2("ljoxqyyw") shouldBe 1074 }
}
