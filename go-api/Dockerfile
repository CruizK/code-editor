FROM golang:1.17 as builder
WORKDIR /app
COPY go.mod go.sum ./
RUN go mod download
COPY . .
ENV CGO_ENABLED=0

RUN COOS=linux go build ./cmd/server 

FROM scratch

WORKDIR /app

COPY --from=builder /app/server .

EXPOSE 8081
CMD ["/app/server"]