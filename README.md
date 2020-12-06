DXF Reader
------------

This library provides basic .DXF parsing into .Net Standard objects. Writing is not supported. Only ASCII formatted files are supported.

DXF is a file format developed by AutoDESK and supported by  CAD software including the free and open source LibreCAD (https://librecad.org/).

The initial focus for this project was to read contained ENTITIES section. Other DXF sections are parsed but may not necessarily be supported fully.


FEATURE SUPPORT
-------------
 SECTION            | SUB ITEMS     | ASCII              |  Binary            |
--------------------|-------------- | ------------------ | ------------------ |
`HEADER`            |               | :white_check_mark: | :x:                |
`CLASSES`           |               | :x:                | :x:                |
`TABLES`            |               | :x:                | :x:                |
`BLOCKS`            |               | :x:                | :x:                |
`ENTITIES`          |`3DFACE`       | :x:                | :x:                |
`ENTITIES`          |`3DSOLID`      | :x:                | :x:                |
`ENTITIES`          |`ARC`          | :white_check_mark: | :x:                |
`ENTITIES`          |`CIRCLE`       | :white_check_mark: | :x:                |
`ENTITIES`          |`ELLIPSE`      | :white_check_mark: | :x:                |
`ENTITIES`          |`LINE`         | :white_check_mark: | :x:                |
`ENTITIES`          |`LWPOLYLINE`   | :white_check_mark: | :x:                |
`ENTITIES`          |`POLYLINE`     | :white_check_mark: | :x:                |
`ENTITIES`          |`SPLINE`       | :x:                | :x:                |
`OBJECTS`           |               | :x:                | :x:                |
`THUMBNAILIMAGE`    |               | :x:                | :x:                |


LICENSE
-------------------

 * GNU AGPLv3