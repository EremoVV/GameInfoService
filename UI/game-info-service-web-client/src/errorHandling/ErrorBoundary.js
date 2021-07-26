import React from "react";

export default class ErrorBoundary extends React.Component {
  constructor(props) {
    super(props);
    this.state = { hasError: false, errorInfo: [] };
  }

  static getDerivedStateFromError(error) {
    console.log("diddevired worked!");
    return { hasError: true };
  }

  componentDidCatch(error, errorInfo) {
    console.log("didcatch worked!");
    this.setState({ errorInfo: error });
  }

  render() {
    console.log(this.state.hasError);
    console.log(this.state.errorInfo);
    if (this.state.hasError) {
      return (
        <div>
          <h3>Error Boundary</h3>
          <h2>{this.state.errorInfo}</h2>
        </div>
      );
    }
    return (
      <div>
        <h3>Error boundary child</h3>
      </div>
    );
    return this.props.children;
  }
}
