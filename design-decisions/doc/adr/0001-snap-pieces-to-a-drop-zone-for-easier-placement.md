# 1. Snap pieces to a drop-zone for easier placement

Date: 2018-11-23

## Status

Accepted

## Context

Seeing as I'm closing in on actually implementing the graphics and the GUI, some calls had to be made on how to proceed with the game. I *think* that it would be nice to give the user *some* help when putting down the pieces, as not to be completely ignorant of how the user can play with the pieces.

The alternative is either to have a complete "tower snap" - that snaps the piece to the closest tower of the current input, but that disallows cool things like losing the towerpiece.

## Decision

I feel like going with the guided approach, but still allow users to drop the pieces wherever else they like would give the best user experience.

## Consequences

This enforces a constraint on *what* is a decent snap-area. And of course, the code would have to adhere to different rules depending on where we went, but I hope this is the right direction.
