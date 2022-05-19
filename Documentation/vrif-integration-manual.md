# VRIF Interaction Component for VR Builder
## Table of Contents

1. [Introduction](#introduction)
1. [Requirements](#requirements)
1. [Quick Start](#quick-start)
1. [Data Properties](#data-properties)
    - [Creating Data Properties](#creating-data-properties)
    - [Data Property Displays](#data-property-displays)
1. [Working with Data Properties](#working-with-data-properties)
    - [Set Value Behaviors](#set-value-behaviors)
    - [Reset Value Behavior](#reset-value-behavior)
    - [Compare Values Conditions](#compare-values-conditions)
    - [Logging Data Properties](#logging-data-properties)
1. [Math Operation Behavior](#math-operation-behavior)
1. [State Data Properties](#state-data-properties)
    - [Creating a State Data Property](#creating-a-state-data-property)
    - [Handling States in Code](#handling-states-in-code)
    - [Set State Behavior](#set-state-behavior)
    - [Check State Condition](#check-state-condition)
1. [Contact](#contact)

## Introduction

This add-on allows to use VR Builder together with VR Interaction Framework. It makes it possible to build a VR Builder process based on a scene using VRIF. 

## Requirements

This add-on requires VR Builder version 2.0.0 or later to work.

## Quick Start


## Grabbable Property
The included `Grabbable Property` allows to use the standard Grab Object and Release Object conditions with the VRIF grabbers and grabbables. Adding it to a game object will automatically add the `Grabbable` and `Grabbable Unity Events` components from VRIF.

Note that a rigidbody is not automatically added to the object, as the `Grabbable` component can be also used without in some cases. However, if you want a "standard" grabbable object, you should add one manually.

## Usable Property
This property qualifies the object as "in use" based on the `onTriggerDown` event on the `Grabbable Unity Events` component.

## Snappable/Snap Zone Property
These allow to use VRIF snap zones in a VR Builder process. Note that snap zones in VRIF, contrary to the one in the default XR Interaction component, don't snap one specific object by default.

Please refer to the VRIF demos and documentation to learn how to unsnap objects and give visual feedback.