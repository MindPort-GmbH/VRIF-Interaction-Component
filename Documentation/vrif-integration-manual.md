# VRIF Interaction Component for VR Builder
## Table of Contents

1. [Introduction](#introduction)
1. [Requirements](#requirements)
1. [Quick Start](#quick-start)
1. [Properties](#data-properties)
    - [Grabbable Property](#grabbable-property)
    - [Touchable Property](#touchable-property)
    - [Usable Property](#usable-property)
    - [Snappable/Snap Zone Property](#snappablesnap-zone-property)
    - [Lever/Wheel Property](#leverwheel-property)
1. [Check Control Position Condition](#check-control-position-condition)
1. [Contact](#contact)

## Introduction

This add-on allows to use VR Builder together with VR Interaction Framework. It makes it possible to build a VR Builder process based on a scene using VRIF. 

## Requirements

This add-on requires VR Builder version 2.1.0 or later to work.

## Quick Start



## Properties

This integration includes the following properties.

### Grabbable Property
The included `Grabbable Property` allows to use the standard Grab Object and Release Object conditions with the VRIF grabbers and grabbables. Adding it to a game object will automatically add the `Grabbable` and `Grabbable Unity Events` components from VRIF.

Note that a rigidbody is not automatically added to the object, as the `Grabbable` component can be also used without in some cases. However, if you want a "standard" grabbable object, you should add one manually.

### Touchable Property
Allows to use the Touch Object condition with VRIF.

### Usable Property
This property qualifies the object as "in use" based on the `onTriggerDown` event on the `Grabbable Unity Events` component.

### Snappable/Snap Zone Property
These allow to use VRIF snap zones in a VR Builder process. Note that snap zones in VRIF, contrary to the one in the default XR Interaction component, don't snap one specific object by default. The `Snap Zone Property` does not perform any automatic configuration on the snap zone itself. Please refer to the VRIF demos and documentation to configure the snap zones.

### Lever/Wheel Property
These can be added to a game object with respectively the `Lever` or `Driving Wheel` component. The object can then be used in the `Check Control Position` condition.

## Check Control Position Condition
### Description

![Check Control Position](images/check-control-position.png)

This condition triggers when a movable object like a lever or a wheel reaches a position within a range. The position is normalized from 0 (down) to 1 (up) for a lever, and from -1 to 1 for a driving wheel.

#### Configuration

- **Control**

    The object whose position we want to check. The game object referenced here should be the one with the `Lever` or `Driving Wheel` component, not necessarily the root object. Please manually add a `Lever Property` or `Wheel Property` instead of relying on the `Fix it` button. As they are implementations of the same interface, it is not possible at this time to automatically select the correct property.

- **Min position**

    The minimum position which will be considered valid by the condition.

- **Max position**

    The maximum position which will be considered valid by the condition.

- **Require release**

    If this is checked, the condition will not complete until the object is released. This will require the user to place the object in the correct position and release it instead of just moving it back and forth until something happens.

## Contact

Join our official [Discord server](http://community.mindport.co) for quick support from the developer and fellow users. Suggest and vote on new ideas to influence the future of the VR Builder.

Make sure to review [VR Builder](https://assetstore.unity.com/packages/tools/visual-scripting/vr-builder-201913) if you like it. It will help us immensely.

If you have any issues, please contact [contact@mindport.co](mailto:contact@mindport.co). We'd love to get your feedback, both positive and constructive. By sharing your feedback you help us improve - thank you in advance!
Let's build something extraordinary!

You can also visit our website at [mindport.co](http://www.mindport.co).