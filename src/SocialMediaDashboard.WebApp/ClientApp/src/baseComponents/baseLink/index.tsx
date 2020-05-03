import React, { Component } from 'react';

interface BaseLinkProps {
	href: string,
	label: string | undefined,
}

class BaseLink extends Component<BaseLinkProps> {
	public static defaultProps = {
		label: undefined,
	}
	render() {
		const { href, label } = this.props;
		return (
			<a href={href}>{label === undefined ? href : label}</a>
		);
	}
}
export default BaseLink;