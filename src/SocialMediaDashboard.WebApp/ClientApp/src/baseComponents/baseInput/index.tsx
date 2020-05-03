import React, { Component } from "react";
import BaseLabel from "../baseLabel";

export enum InputTypes {
	text = "text",
	password = "password"
}

interface BaseInputInterface {

    /**
     * Text of label near the input.
     */
	labelText: string | undefined

    /**
     * Input type. (e.g. text/password)
     */
	inputType: InputTypes,

    /**
     * Input event.
     */
	onInput: (event: React.FormEvent<HTMLInputElement>) => void,

    /**
     * Default input value.
     */
	defaultInputValue: string | undefined
}

class BaseInput extends Component<BaseInputInterface> {
	public static defaultProps = {
		labelText: undefined,
		defaultInputValue: undefined,
	}

	render() {
		const {
			labelText,
			inputType,
			onInput,
			defaultInputValue
		} = this.props;
		return (
			<>
				{labelText !== undefined ? <BaseLabel labelText={labelText}></BaseLabel> : null}
				<input defaultValue={defaultInputValue} type={inputType} onInput={onInput} />
			</>
		);
	}
	test(): (event: React.FormEvent<HTMLInputElement>) => void {
		throw new Error("Method not implemented.");
	}
}

export default BaseInput;