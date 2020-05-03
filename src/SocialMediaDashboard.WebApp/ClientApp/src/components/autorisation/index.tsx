import React, { Component } from "react";
import { connect } from "react-redux";
import { ApplicationState } from "../../store";
import { AurorisationState } from "./reducer";
import { actionCreators } from "./actions";

import BaseInput, { InputTypes } from "../../baseComponents/baseInput";

import "./styles/auth.scss";
import BaseLabel from "../../baseComponents/baseLabel";

type AutorisationProps =
	AurorisationState &
	typeof actionCreators;
export class Autorisation extends Component<AutorisationProps> {
	constructor(props: Readonly<AutorisationProps>) {
		super(props);

		this.onLoginSubmit = this.onLoginSubmit.bind(this);
	}
	state = {
		login: "",
		password: "",
	}

	render() {
		const { isLoggedIn } = this.props;
		return (
			<React.Fragment>
				{isLoggedIn ?
					(
						<div className="authContainer">
							<div className="authLabelContainer">
								<BaseLabel labelText="You are logged in!"></BaseLabel>
								<button onClick={(event) => this.onLogoutSubmit(event)}>Logout</button>
							</div>
						</div>
					)
					:
					(
						<div className="authContainer">
							<BaseInput labelText="Login:" onInput={(event) => this.onLoginInput(event)} inputType={InputTypes.text} />
							<div className="authLabelContainer">
								<BaseInput labelText="Password:" onInput={(event) => this.onPasswordInput(event)} inputType={InputTypes.password} />
							</div>
							<button className="authSumbitButtom" onClick={(event) => this.onLoginSubmit(event)}>Login</button>
						</div>
					)
				}
			</React.Fragment>
		);
	}
	//TODO: Replace to event handler
	onLoginSubmit(event: React.MouseEvent) {
		const {
			login,
			password
		} = this.state;
		this.props.onLogin(login, password);
	}

	onLogoutSubmit(event: React.MouseEvent) {
		this.props.onLogOut();
	}

	onLoginInput(event: React.FormEvent<HTMLInputElement>) {
		const newValue = (event.target as HTMLTextAreaElement).value;
		this.setState({
			login: newValue
		});
	}

	onPasswordInput(event: React.FormEvent<HTMLInputElement>) {
		const newValue = (event.target as HTMLTextAreaElement).value;
		this.setState({
			password: newValue
		});
	}

}

export default connect(
	(state: ApplicationState) => state.autorisation,
	actionCreators
)(Autorisation);
