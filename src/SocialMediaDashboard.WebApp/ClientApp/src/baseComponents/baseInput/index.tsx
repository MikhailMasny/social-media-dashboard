import React, { Component } from "react";
import BaseLabel from "../baseLabel";

export enum InputTypes {
	text = "text",
	password = "password"
}

interface BaseInputProps {

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
	onInput: (event: React.FormEvent<HTMLInputElement>) => void | undefined,

    /**
     * Default input value.
     */
	defaultInputValue: string | undefined

	/**
	 * Disable input.
	 */
	disabled: boolean,
}

/**
 * Base application input.
 */
class BaseInput extends Component<BaseInputProps> {
	public static defaultProps = {
		labelText: undefined,
		defaultInputValue: undefined,
		disabled: false,
		onInput: undefined,
	}

	render() {
		const {
			labelText,
			inputType,
			onInput,
			defaultInputValue,
			disabled
		} = this.props;
		return (
			<>
				{labelText !== undefined ? <BaseLabel labelText={labelText}></BaseLabel> : null}
				<input defaultValue={defaultInputValue} type={inputType} onInput={onInput} disabled={disabled} />
			</>
		);
	}
}

export default BaseInput;