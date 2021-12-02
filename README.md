# NVAN X-Plane PoKeys Connector
Interface PoKeys with X-Plane DataRefs and Commands *(at the moment, it only supports Ethernet versions)*

It's a totally stable solution, that does not require any plug-in to be installed.

## Donations
If you use and like this program, consider buying me a 🍺 beer. You can donate via **PayPal**:

[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://paypal.me/maduranma)

## Installation
Installation is simple, just download the [latest version here](https://github.com/nvan/x-plane-pokeys-connector/releases) and double click.

Then you have to set the IP Address of the PoKeys card (check it in the PoKeys Configuration program) and the X-Plane IP (use `127.0.0.1` if it's in the same computer).

You don't need any X-plane plug-in, so just click on `Add` to start adding Inputs and Outputs!

## Usage

As soon as you open the program, you'll get a list of the active Inputs and Outputs and you'll be able to manage them even with the simulator connected (real time).

### DataRefs and Commands
If you want to use a **Command** instead of a **DataRef** just place a `%` symbol before the **Command**.

I.E: `%sim/operation/pause_toggle` will send the pause **Command**.

When a **Command** is used, `Write Value` will be ignored.

### Input and outputs

You can interface different types of inputs and outputs by changing the pin type:

- **PIN**: Single digital input or output, place what your condition in *Read Value* and what you'll write to the *DataRef* to *Write Value*.
- **POEXTBUS**: Only for outputs, it supports up to 80 outputs of pheriperals connected to the *PoExtBus*.
- **ANALOG**: Only for inputs, you'll be able to read a value and MAP it before sending to the *DataRef*. *I.E: if you map 0-4096 to 0-100, if you read 2048, then write value will be 50. Select `Round Float to Int` if you only want integer numbers (removing decimals).*
- **ENCODER**: Read and send the encoder counter value directly to a *DataRef*. The *offset* is a displacement if you need it to be lower or higher at the beggining (can be positive or negative).
- **DUAL**: Dual digital inputs, to use for example in most triple switches (like No Smoking OFF-AUTO-ON) which commonly uses 2 pins and you get different combinations. Place every combination to send every different value to a *DataRef*.

On the inputs, you'll be able to check `Sync on Connect` so when you connect the simulator, it'll take the current position of the switches, without having the need to reset all the panels every time (just like in real life, everything it's like it was when you left it).

## Issues and contributions
Please, feel free to ask for questions or features, or report bugs in the **Issues** section.

Also, feel free to **Contribute** by making a **Pull Request**.

Even if the code is not the best you'll see, as this has been a fast project to make our own simulator work, it's proven to be totally stable (0 crashes) and it's tought to be fast (optimizing and caching *DataRefs* for each cycle, avoiding saturation) and it doesn't affect the simulator performance in anyway, even better because it's not a plug-in but an external tool that can even run on a remote computer via network.

## Tested in our own A320 Simulator
We are building an A320 Flight Simulator in Mallorca, check it on [Instagram](https://www.instagram.com/a320flightsimulator/)!

All OEM panels in Overhead, MIP and Pedestal (excluding flight controls that act as joysticks via Arduino) have been interfaced with this program using Flight Factor's A320.

## License
MIT

This program is free, but please, give me credit if you use it!
