import React from 'react';

/**
 * Footer props.
 */
interface FooterProps {
	/**
	 * Footer components.
	 */
	childs: [React.ReactChild] | null,
}

/**
 * Application footer.
 */
class Footer extends React.Component<FooterProps> {
	public static defaultProps = {
		childs: null
	}

	render() {
		const { childs } = this.props;

		return (
			<footer>
				<div>I am footer.</div>
				{childs !== null ? { ...childs } : null}
			</footer>
		)
	}
}

export default Footer;