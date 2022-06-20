# VRIF Interaction Component for VR Builder

## Introduction

VR Builder is a tool for quickly creating sequential interactive processes, like for example tutorials, by using an easy to use visual editor. This add-on allows to use VR Builder together with VR Interaction Framework. It makes it possible for VR Builder to know when the user grabs something, if a lever has been pulled and so on. This way, it is possible to replace VR Builder's default interaction framework (XRIT) with VRIF, in order to leverage the unique strengths of the latter.

## Installation

### Requirements
- This add-on requires VR Builder version 2.2.0 or later to work.
- This add-on is based on VRIF 1.81. Earlier versions might work, but they are untested.

### How to Install

1. First, ensure that both VRIF and VR Builder are present in the project.
1. As you are using a different interaction component, it is recommended to disable VR Builder's default interaction component. To do so, open `Project Settings > VR Builder > Settings` and uncheck `Enable built-in XR Interaction Component`.
1. Navigate to `Assets/BNG Framework/Integrations/VR Builder` and import the Unity package for this add-on.
1. Done! Now you can either open the demo scene from `Tools > VR Builder > Demo Scenes > VRIF Integration` or create a new VR Builder scene using the wizard by selecting `Tools > VR Builder > New Process Wizard...`.

Note: VR Builder imports the XR Interaction Toolkit as a requirement for the built-in interaction component. If you don't need it, it is possible to remove it from the Package Manager after disabling the interaction component.

## Documentation

You can find comprehensive documentation in the `Assets/MindPort/VR Builder/Add-ons/VRIF Interaction Component` folder.

## Contact

Join our official [Discord server](http://community.mindport.co) for quick support from the developer and fellow users. Suggest and vote on new ideas to influence the future of the VR Builder.

Make sure to review [VR Builder](https://assetstore.unity.com/packages/tools/visual-scripting/vr-builder-201913) if you like it. It will help us immensely.

If you have any issues, please contact [contact@mindport.co](mailto:contact@mindport.co). We'd love to get your feedback, both positive and constructive. By sharing your feedback you help us improve - thank you in advance!
Let's build something extraordinary!

You can also visit our website at [mindport.co](http://www.mindport.co).
