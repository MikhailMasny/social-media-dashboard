import React, { Component } from 'react';

/**
 * BaseLink props.
 */
interface BaseLinkProps {
	/**
	 * Redirrect link.
	 */
	href: string,

	/**
	 * Link label.
	 */
	label: string | undefined,
}

/**
 * Base application link.
 */
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