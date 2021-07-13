import { useEffect } from "react";

export default function ErrorComp() {
  useEffect(() => {
    throw new Error("someError");
  }, []);

  return <h3>Lol</h3>;
}
