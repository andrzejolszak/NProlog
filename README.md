# NProlog
C# implementation of Prolog ported from Java implementaion Projog

Clp is not included.

Mockito tests are disabled.

Most of tests are passed, you may use it safely.

New Features:
Support Chinese/Unicode Atoms/Variables.

Prolog uses lower case letters to compose Atoms, and upper case letters for Variables.

Languages such as Chinese has no meaning of lower/upper cases which makes no way to distinguish Atom/Variables.

So we added a prefix "@" in front of a variables to indicate it.

So that we can write:
-: assert(like(a,b)).
-: assert(like(a,c)).
likes(a,@哪一个).

also,
-: assert(喜欢(小明,小红)).
-: assert(喜欢(小明,小黄)).
-: 喜欢(小明,@谁).

Pls try it out.
-----------------


# [projog](http://projog.org "Prolog interpreter for Java")
[![Maven Central](https://img.shields.io/maven-central/v/org.projog/projog-core.svg)](https://search.maven.org/search?q=g:org.projog)
[![Build Status](https://travis-ci.org/s-webber/projog.png?branch=master)](https://travis-ci.org/s-webber/projog)
[![License](https://img.shields.io/badge/license-Apache%20v2.0-blue.svg)](http://www.apache.org/licenses/LICENSE-2.0)

## About

Projog provides an implementation of the [Prolog](https://en.wikipedia.org/wiki/Prolog) programming language for the Java platform. Prolog is a declarative logic programming language where programs are represented as facts and rules.

Projog can be used as a stand-alone console application or embedded in your Java applications as a Maven dependency.

## Resources

- [Frequently Asked Questions](http://projog.org/faq.html)
- [Getting Started](http://projog.org/getting-started.html)
- [Calling Prolog from Java](http://projog.org/calling-prolog-from-java.html)
- [Extending Prolog using Java](http://projog.org/extending-prolog-with-java.html)
- Example applications: [Prolog Expert System](https://github.com/s-webber/prolog-expert-system) and [Prolog Wumpus World](https://github.com/s-webber/prolog-wumpus-world)
- [Class diagrams](http://projog.org/class-diagrams.html) and [design decisions](http://projog.org/design-decisions.html)

## Quick Start Guide

The following commands will download Projog and start the console:

```sh
$ wget http://projog.org/downloads/projog-0.8.0.zip
$ jar xvf projog-0.8.0.zip
$ cd projog-0.8.0
$ chmod u+x projog-console.sh
$ ./projog-console.sh
```

When the console has started you can enter the following command:

```
W=X, X=1+1, Y is W, Z is -W.
```

Which should generate the following response:

```
W = 1 + 1
X = 1 + 1
Y = 2
Z = -2

yes
```

To exit the console type `quit.`

## Maven Artifacts

To include Projog within your project, just add this dependency to your `pom.xml` file:

```
<dependency>
   <groupId>org.projog</groupId>
   <artifactId>projog-core</artifactId>
   <version>0.8.0</version>
</dependency>
```

## Reporting Issues

We would be grateful for feedback. If you would like to report a bug, suggest an enhancement or ask a question then please [create a new issue](https://github.com/s-webber/projog/issues/new).
