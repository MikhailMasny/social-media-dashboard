import * as React from "react";
import BaseLink from "../../baseComponents/baseLink";
import "./style.scss";

export default class NavMenu extends React.PureComponent<{}, { isOpen: boolean }> {
	public state = {
		isOpen: false,
		navLinks: [
			{
				href: "/",
				label: "Home"
			},
			{
				href: "/",
				label: "Home (again)"
			}
		]
	};

	public render() {
		const {
			navLinks
		} = this.state;
		return (
			<header>
				<div>
					{navLinks.map(navLink => (
						<BaseLink href={navLink.href} label={navLink.label} />
					))}
				</div>
			</header>
		);
	}

	private toggle = () => {
		this.setState({
			isOpen: !this.state.isOpen
		});
	}
}
