import React, { Component } from "react";

interface BaseLabelInterface {
	labelText: string
}

class BaseLabel extends Component<BaseLabelInterface> {
	render() {
		const { labelText } = this.props;
		return (
			<label>{labelText}</label>
		);
	}
}

export default BaseLabel;